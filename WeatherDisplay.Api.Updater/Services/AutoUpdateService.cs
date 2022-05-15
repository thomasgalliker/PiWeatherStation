using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NuGet.Versioning;
using WeatherDisplay.Api.Updater.Models;

namespace WeatherDisplay.Api.Updater.Services
{
    public class AutoUpdateService : BackgroundService, IAutoUpdateService
    {
        private static readonly SemanticVersion DebugVersion = new SemanticVersion(1, 0, 0);
        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented
        };

        private readonly ILogger logger;
        private readonly AutoUpdateOptions autoUpdateOptions;
        private readonly IHostApplicationLifetime hostApplicationLifecycle;
        private readonly IProcessFactory processFactory;
        private readonly HttpClient httpClient;

        public AutoUpdateService(
            ILogger<AutoUpdateService> logger,
            AutoUpdateOptions autoUpdateOptions,
            IHostApplicationLifetime hostApplicationLifecycle)
            : this(logger, autoUpdateOptions, hostApplicationLifecycle, new SystemProcessFactory(), new HttpClient())
        {
        }

        public AutoUpdateService(
            ILogger<AutoUpdateService> logger,
            AutoUpdateOptions autoUpdateOptions,
            IHostApplicationLifetime hostApplicationLifecycle,
            IProcessFactory processFactory,
            HttpClient httpClient)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.autoUpdateOptions = autoUpdateOptions;
            this.hostApplicationLifecycle = hostApplicationLifecycle;
            this.processFactory = processFactory;
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var result = await this.CheckForUpdateAsync();
            if (result.HasUpdate)
            {
                await this.InstallUpdateAsync(result.UpdateVersion);
            }
        }

        public async Task<UpdateCheckResult> CheckForUpdateAsync()
        {
            this.logger.LogInformation("CheckForUpdateAsync");

            UpdateCheckResult result;
            try
            {
                var installedVersionFile = this.autoUpdateOptions.InstalledVersionFile;
                var localSemanticVersion = await GetInstalledVersionAsync(installedVersionFile);
                if (localSemanticVersion == DebugVersion)
                {
                    //return UpdateCheckResult.NoUpdateAvailable;
                }

                var remoteVersion = await this.GetLatestVersionAsync(this.autoUpdateOptions.PreRelease);
                var remoteSemanticVersion = SemanticVersion.Parse(remoteVersion.TagName);

                if (localSemanticVersion < remoteSemanticVersion)
                {
                    result = new UpdateCheckResult(remoteVersion);
                }
                else
                {
                    result = UpdateCheckResult.NoUpdateAvailable;
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "CheckForUpdateAsync failed with exception");
                throw;
            }

            this.logger.LogInformation($"CheckForUpdateAsync finished with result.HasUpdate={result.HasUpdate}");
            return result;
        }

        public async Task InstallUpdateAsync(GithubVersionDto updateVersion)
        {
            this.logger.LogInformation($"InstallUpdateAsync: TagName={updateVersion.TagName}");

            try
            {
                var currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var downloadUrl = updateVersion.Assets.First().DownloadUrl;
                this.logger.LogInformation($"CheckForUpdateAsync: Starting update with currentDirectory={currentDirectory}, downloadUrl={downloadUrl}");

                var updateRequestDto = new UpdateRequestDto
                {
                    DownloadUrl = downloadUrl,
                    WorkingDirectory = currentDirectory,
                    ExecutorSteps = new IExecutorStep[]
                    {
                        new DownloadFileStep
                        {
                            //Url = "blabla",
                        },
                        new ExtractZipStep
                        {
                            //Url = "blabla",
                        },
                        new ProcessStartExecutorStep
                        {
                            FileName = "sudo",
                            Arguments = "systemctl stop weatherdisplay.api.service",
                            CreateNoWindow = true,
                        },
                        new ProcessStartExecutorStep
                        {
                            FileName = "sudo",
                            Arguments = "systemctl start weatherdisplay.api.service",
                            CreateNoWindow = true,
                        }
                    }
                };

                var updateRequestJson = JsonConvert.SerializeObject(updateRequestDto);
                var updateRequestJsonBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(updateRequestJson));

                var processStartInfo = new ProcessStartInfo
                {
                    FileName = this.autoUpdateOptions.DotnetExecutable,
                    Arguments = $"{this.autoUpdateOptions.UpdaterExecutable} {updateRequestJsonBase64} --debug",
                    RedirectStandardOutput = false,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                };
                var process = this.processFactory.CreateProcess(processStartInfo);
                var result = process.Start();
                process.WaitForExit();
                this.logger.LogInformation($"CheckForUpdateAsync: Update finished with exit code {process.ExitCode}");

                await UpdateInstalledVersionAsync(this.autoUpdateOptions.InstalledVersionFile, updateVersion);

                //this.hostApplicationLifecycle.StopApplication();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "InstallUpdateAsync failed with exception");
            }
        }

        public async Task<GithubVersionDto> GetLatestVersionAsync(bool prerelease)
        {
            GithubVersionDto latestVersionDto;

            if (prerelease)
            {
                var githubVersionDtos = await this.httpClient.GetFromJsonAsync<IEnumerable<GithubVersionDto>>(this.autoUpdateOptions.GithubRepositoryUrl);
                latestVersionDto = githubVersionDtos.FirstOrDefault();
            }
            else
            {
                latestVersionDto = await this.httpClient.GetFromJsonAsync<GithubVersionDto>(this.autoUpdateOptions.GithubRepositoryUrl + "/latest");
            }

            return latestVersionDto;
        }

        private static async Task UpdateInstalledVersionAsync(string installedVersionFile, GithubVersionDto githubLatestVersionDto)
        {
            var jsonContent = JsonConvert.SerializeObject(githubLatestVersionDto, JsonSerializerSettings);
            await File.WriteAllTextAsync(installedVersionFile, jsonContent);
        }

        private static async Task<SemanticVersion> GetInstalledVersionAsync(string installedVersionFile)
        {
            string productVersion = null;

            if (!File.Exists(installedVersionFile))
            {
                productVersion = GetProductVersion();
            }
            else
            {
                try
                {
                    var jsonContent = await File.ReadAllTextAsync(installedVersionFile);
                    if (!string.IsNullOrEmpty(jsonContent))
                    {
                        var githubLatestVersionDto = JsonConvert.DeserializeObject<GithubVersionDto>(jsonContent, JsonSerializerSettings);
                        productVersion = githubLatestVersionDto.TagName;
                    }
                }
                finally
                {
                    if (productVersion == null)
                    {
                        productVersion = GetProductVersion();
                    }
                }
            }

            return SemanticVersion.Parse(productVersion);
        }

        private static string GetProductVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fileVersionInfo.ProductVersion;
        }
    }
}

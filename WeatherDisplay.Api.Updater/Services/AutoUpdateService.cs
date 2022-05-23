using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NuGet.Versioning;
using WeatherDisplay.Api.Updater.Models;

namespace WeatherDisplay.Api.Updater.Services
{
    public class AutoUpdateService : IAutoUpdateService
    {
        private static readonly SemanticVersion DebugVersion = new SemanticVersion(1, 0, 0);
        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented
        };

        private readonly ILogger logger;
        private readonly AutoUpdateOptions autoUpdateOptions;
        private readonly IProcessFactory processFactory;
        private readonly HttpClient httpClient;

        public AutoUpdateService(
            ILogger<AutoUpdateService> logger,
            AutoUpdateOptions autoUpdateOptions)
            : this(logger, autoUpdateOptions, new SystemProcessFactory(), new HttpClient())
        {
        }

        public AutoUpdateService(
            ILogger<AutoUpdateService> logger,
            AutoUpdateOptions autoUpdateOptions,
            IProcessFactory processFactory,
            HttpClient httpClient)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.autoUpdateOptions = autoUpdateOptions;
            this.processFactory = processFactory;
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
        }

        public async Task<UpdateCheckResult> CheckForUpdateAsync()
        {
            this.logger.LogInformation("CheckForUpdateAsync");

            UpdateCheckResult result;
            try
            {
                var installedVersionFile = this.autoUpdateOptions.InstalledVersionFile;
                var localSemanticVersion = await GetInstalledVersionAsync(installedVersionFile);
#if DEBUG
                if (localSemanticVersion == DebugVersion)
                {
                    return UpdateCheckResult.NoUpdateAvailable;
                }
#endif

                var remoteVersion = await this.GetLatestVersionAsync(this.autoUpdateOptions.PreRelease);
                var remoteSemanticVersion = SemanticVersion.Parse(remoteVersion.TagName);

                var remoteVersionIsNewer = localSemanticVersion < remoteSemanticVersion;
                this.logger.LogDebug($"CheckForUpdateAsync compares local version {localSemanticVersion} < remove version {remoteSemanticVersion} = {remoteVersionIsNewer}");
                
                if (remoteVersionIsNewer)
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

        public void StartUpdate(GithubVersionDto updateVersion)
        {
            this.logger.LogInformation($"InstallUpdateAsync: TagName={updateVersion.TagName}");

            try
            {
                var updaterDirectoryName = this.autoUpdateOptions.UpdaterDirectoryName;
                var currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var updateDirectory = new DirectoryInfo(Path.Combine(currentDirectory, updaterDirectoryName)).FullName;
                var downloadUrl = updateVersion.Assets.First().DownloadUrl;
                var downloadFileName = Path.GetFileName(downloadUrl);

                var currentProcess = this.processFactory.GetCurrentProcess();
                var currentProcessId = currentProcess.Id;

                var updateRequestDto = new UpdateRequestDto
                {
                    DownloadUrl = downloadUrl,
                    WorkingDirectory = updateDirectory,
                    CallingProcessId = currentProcessId,
                    ExecutorSteps = new IExecutorStep[]
                    {
                        new DownloadFileStep
                        {
                            Url = downloadUrl,
                            DestinationFileName = downloadFileName
                        },
                        new ProcessStartExecutorStep
                        {
                            FileName = "sudo",
                            Arguments = "systemctl stop weatherdisplay.api.service",
                            CreateNoWindow = true,
                        },
                        new ExtractZipStep
                        {
                            SourceArchiveFileName = downloadFileName,
                            DestinationDirectoryName = currentDirectory,
                            OverwriteFiles = true,
                            DeleteSourceArchive = true,
                        },
                        new ProcessStartExecutorStep
                        {
                            FileName = "sudo",
                            Arguments = "systemctl daemon-reload",
                            CreateNoWindow = true,
                        },
                        new ProcessStartExecutorStep
                        {
                            FileName = "sudo",
                            Arguments = "systemctl start weatherdisplay.api.service",
                            CreateNoWindow = true,
                        },
                        //new ProcessStartExecutorStep
                        //{
                        //    FileName = "sudo",
                        //    Arguments = "reboot",
                        //    CreateNoWindow = true,
                        //}
                    }
                };

                var updateRequestJson = JsonConvert.SerializeObject(updateRequestDto);
                var updateRequestJsonBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(updateRequestJson));


                this.logger.LogInformation($"InstallUpdateAsync: Preparing update directory {updateDirectory}");
                if (!Directory.Exists(updateDirectory))
                {
                    Directory.CreateDirectory(updateDirectory);
                }

                var updateExecutableFile = new FileInfo(Path.Combine(currentDirectory, this.autoUpdateOptions.UpdaterExecutable));
                var updateExecutableFileCopy = Path.Combine(updateDirectory, this.autoUpdateOptions.UpdaterExecutable);
                this.logger.LogInformation($"InstallUpdateAsync: Copied {updateExecutableFile} to {updateExecutableFileCopy}");

                var pattern = updateExecutableFile.Name.Remove(updateExecutableFile.Name.Length - updateExecutableFile.Extension.Length);
                CopyFilesToUpdateDirectory(currentDirectory, updateDirectory, pattern);
                CopyFilesToUpdateDirectory(currentDirectory, updateDirectory, "Newtonsoft.Json.dll");

                var command = $"{this.autoUpdateOptions.DotnetExecutable} {updateExecutableFileCopy} {updateRequestJsonBase64}";

                var processStartInfo = new ProcessStartInfo
                {
                    FileName = "sudo",
                    Arguments = $"sh -c \"{command}\"",
                    //Arguments = $"systemd-run --scope {command}",
                    //FileName = this.autoUpdateOptions.DotnetExecutable,
                    //Arguments = $"{updateExecutableFileCopy} {updateRequestJsonBase64}",
                    RedirectStandardOutput = false,
                    RedirectStandardError = false,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                };
                var process = this.processFactory.CreateProcess(processStartInfo);

                this.logger.LogInformation($"InstallUpdateAsync: Starting update executor process...");
                process.Start();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "InstallUpdateAsync failed with exception");
            }
        }

        private static void CopyFilesToUpdateDirectory(string currentDirectory, string updateDirectory, string pattern)
        {
            Directory.GetFiles(currentDirectory, $"{pattern}*", SearchOption.TopDirectoryOnly).ToList().ForEach(f =>
            {
                var sourceFile = new FileInfo(f);
                var destinationFile = Path.Combine(updateDirectory, sourceFile.Name);
                File.Copy(sourceFile.FullName, destinationFile, overwrite: true);
            });
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

        private static void Log(string message)
        {
            Console.WriteLine($"UpdateExecutorService: {message}");
        }
    }
}

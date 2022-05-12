﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NuGet.Versioning;
using WeatherDisplay.Api.Updater.Models;

namespace WeatherDisplay.Api.Updater.Services
{
    public class AutoUpdateService : BackgroundService, IAutoUpdateService
    {
        private static readonly SemanticVersion DebugVersion = new SemanticVersion(1, 0, 0);
        private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
        {
            WriteIndented = true
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
                    return new UpdateCheckResult(remoteVersion);
                }
                else
                {
                    return UpdateCheckResult.NoUpdateAvailable;
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "CheckForUpdateAsync failed with exception");
                throw;
            }
        }

        public async Task InstallUpdateAsync(GithubVersionDto updateVersion)
        {
            try
            {
                var currentDirectory = Environment.CurrentDirectory;
                var downloadUrl = updateVersion.Assets.First().DownloadUrl;
                this.logger.LogInformation($"CheckForUpdateAsync: Starting update with currentDirectory={currentDirectory}, downloadUrl={downloadUrl}");

                var updateRequestDto = new UpdateRequestDto
                {
                    DownloadUrl = downloadUrl,
                    WorkingDirectory = currentDirectory,
                };

                var updateRequestJson = JsonSerializer.Serialize(updateRequestDto);
                var updateRequestJsonBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(updateRequestJson));

                var processStartInfo = new ProcessStartInfo
                {
                    FileName = this.autoUpdateOptions.DotnetExecutable,
                    Arguments = $"{this.autoUpdateOptions.UpdaterExecutable} {updateRequestJsonBase64}",
                    RedirectStandardOutput = false,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                };
                var process = this.processFactory.CreateProcess(processStartInfo);
                var result = process.Start();
                process.WaitForExit();
                this.logger.LogInformation($"CheckForUpdateAsync: Update finished with exit code {process.ExitCode}");

                await UpdateInstalledVersionAsync(this.autoUpdateOptions.InstalledVersionFile, updateVersion);

                //Environment.Exit(-1);
                this.hostApplicationLifecycle.StopApplication();
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
            var jsonContent = JsonSerializer.Serialize(githubLatestVersionDto, JsonSerializerOptions);
            await File.WriteAllTextAsync(installedVersionFile, jsonContent);
        }

        private static async Task<SemanticVersion> GetInstalledVersionAsync(string installedVersionFile)
        {
            string productVersion;

            if (!File.Exists(installedVersionFile))
            {
                productVersion = GetProductVersion();
            }
            else
            {
                var jsonContent = await File.ReadAllTextAsync(installedVersionFile);
                var githubLatestVersionDto = JsonSerializer.Deserialize<GithubVersionDto>(jsonContent, JsonSerializerOptions);
                productVersion = githubLatestVersionDto.TagName;
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

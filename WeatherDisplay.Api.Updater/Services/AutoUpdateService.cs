using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
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
        private readonly ILocalVersionChecker localVersionChecker;
        private readonly IRemoteVersionChecker remoteVersionChecker;
        private readonly IProcessFactory processFactory;

        public AutoUpdateService(
            ILogger<AutoUpdateService> logger,
            AutoUpdateOptions autoUpdateOptions,
            ILocalVersionChecker localVersionChecker,
            IRemoteVersionChecker remoteVersionChecker)
            : this(logger, autoUpdateOptions, localVersionChecker, remoteVersionChecker, new SystemProcessFactory())
        {
        }

        public AutoUpdateService(
            ILogger<AutoUpdateService> logger,
            AutoUpdateOptions autoUpdateOptions,
            ILocalVersionChecker localVersionChecker,
            IRemoteVersionChecker remoteVersionChecker,
            IProcessFactory processFactory)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.autoUpdateOptions = autoUpdateOptions;
            this.localVersionChecker = localVersionChecker;
            this.remoteVersionChecker = remoteVersionChecker;
            this.processFactory = processFactory;
        }

        public async Task<UpdateCheckResult> CheckForUpdateAsync()
        {
            this.logger.LogInformation("CheckForUpdateAsync");

            UpdateCheckResult result;
            try
            {
                var localSemanticVersion = await this.localVersionChecker.GetLocalVersionAsync();
                var versionSource = await this.remoteVersionChecker.GetLatestVersionAsync();
                var remoteSemanticVersion = versionSource.Version;

#if DEBUG
                if (localSemanticVersion == DebugVersion)
                {
                    return new UpdateCheckResult(localSemanticVersion);
                }
#endif
                var remoteVersionIsNewer = localSemanticVersion < remoteSemanticVersion;
                this.logger.LogDebug($"CheckForUpdateAsync compares local version {localSemanticVersion} < remove version {remoteSemanticVersion} = {remoteVersionIsNewer}");

                if (remoteVersionIsNewer)
                {
                    result = new UpdateCheckResult(localSemanticVersion, versionSource);
                }
                else
                {
                    return new UpdateCheckResult(localSemanticVersion, remoteSemanticVersion);
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

        public void StartUpdate(UpdateRequest updateRequest)
        {
            this.logger.LogInformation($"StartUpdate: updateVersion={updateRequest.UpdateVersion}, ExecutorSteps={{{updateRequest.ExecutorSteps.Length}}}");

            try
            {
                var updaterDirectoryName = this.autoUpdateOptions.UpdaterDirectoryName;
                var currentDirectory = updateRequest.CurrentDirectory;
                var updateDirectory = new DirectoryInfo(Path.Combine(currentDirectory, updaterDirectoryName)).FullName;

                var currentProcess = this.processFactory.GetCurrentProcess();
                var currentProcessId = currentProcess.Id;

                var updateRequestDto = new UpdateRequestDto
                {
                    WorkingDirectory = updateDirectory,
                    CallingProcessId = currentProcessId,
                    ExecutorSteps = updateRequest.ExecutorSteps,
                };

                var updateRequestJson = JsonConvert.SerializeObject(updateRequestDto);
                var updateRequestJsonBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(updateRequestJson));


                this.logger.LogInformation($"StartUpdate: Preparing update directory {updateDirectory}");
                if (!Directory.Exists(updateDirectory))
                {
                    Directory.CreateDirectory(updateDirectory);
                }

                var updateExecutableFile = new FileInfo(Path.Combine(currentDirectory, this.autoUpdateOptions.UpdaterExecutable));
                var updateExecutableFileCopy = Path.Combine(updateDirectory, this.autoUpdateOptions.UpdaterExecutable);
                this.logger.LogInformation($"StartUpdate: Copied {updateExecutableFile} to {updateExecutableFileCopy}");

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

                this.logger.LogInformation($"StartUpdate: Starting update executor process...");
                process.Start();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "StartUpdate failed with exception");
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


        private static async Task UpdateInstalledVersionAsync(string installedVersionFile, GithubVersionDto githubLatestVersionDto)
        {
            var jsonContent = JsonConvert.SerializeObject(githubLatestVersionDto, JsonSerializerSettings);
            await File.WriteAllTextAsync(installedVersionFile, jsonContent);
        }

    }
}

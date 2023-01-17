using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        private static readonly SemanticVersion LocalDebugVersion = new SemanticVersion(1, 0, 0);

        private readonly ILogger logger;
        private readonly AutoUpdateOptions options;
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
            this.options = autoUpdateOptions;
            this.localVersionChecker = localVersionChecker;
            this.remoteVersionChecker = remoteVersionChecker;
            this.processFactory = processFactory;
        }

        public async Task<UpdateCheckResult> CheckForUpdateAsync(bool force = false)
        {
            this.logger.LogInformation("CheckForUpdateAsync");

            UpdateCheckResult result;
            try
            {
                var localSemanticVersion = await this.localVersionChecker.GetLocalVersionAsync();
                var updateVersionSource = await this.remoteVersionChecker.GetLatestVersionAsync();
                var remoteSemanticVersion = updateVersionSource.Version;

                if (force)
                {
                    this.logger.LogDebug($"CheckForUpdateAsync forces remote version {remoteSemanticVersion} over local version {localSemanticVersion}");
                    result = new UpdateCheckResult(localSemanticVersion, updateVersionSource);
                }
                else
                {
                    if (localSemanticVersion == LocalDebugVersion)
                    {
                        this.logger.LogDebug($"CheckForUpdateAsync skipping since local version {localSemanticVersion} is a debug version");
                        return new UpdateCheckResult(localSemanticVersion);
                    }

                    if (localSemanticVersion < remoteSemanticVersion)
                    {
                        this.logger.LogDebug($"CheckForUpdateAsync found remote version {remoteSemanticVersion} to be newer than local version {localSemanticVersion}");
                        result = new UpdateCheckResult(localSemanticVersion, updateVersionSource);
                    }
                    else
                    {
                        this.logger.LogDebug($"CheckForUpdateAsync has not found a version newer than local version {remoteSemanticVersion}");
                        return new UpdateCheckResult(localSemanticVersion, remoteSemanticVersion);
                    }
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
                var updaterDirectoryName = this.options.UpdaterDirectoryName;
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

                var updateExecutableFile = new FileInfo(Path.Combine(currentDirectory, this.options.UpdaterExecutable));
                var updateExecutableFileCopy = Path.Combine(updateDirectory, this.options.UpdaterExecutable);
                this.logger.LogInformation($"StartUpdate: Copied {updateExecutableFile} to {updateExecutableFileCopy}");

                var pattern = updateExecutableFile.Name.Remove(updateExecutableFile.Name.Length - updateExecutableFile.Extension.Length);
                CopyFilesToUpdateDirectory(currentDirectory, updateDirectory, pattern);
                CopyFilesToUpdateDirectory(currentDirectory, updateDirectory, "Newtonsoft.Json.dll");

                var command = $"{this.options.DotnetExecutable} {updateExecutableFileCopy} {updateRequestJsonBase64}";

                var processStartInfo = new ProcessStartInfo
                {
                    FileName = "sudo",
                    Arguments = $"sh -c \"{command}\"",
                    //Arguments = $"systemd-run --scope {command}",
                    //FileName = this.options.DotnetExecutable,
                    //Arguments = $"{updateExecutableFileCopy} {updateRequestJsonBase64}",
                    RedirectStandardOutput = false,
                    RedirectStandardError = false,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                };
                var process = this.processFactory.CreateProcess(processStartInfo);

                this.logger.LogInformation($"StartUpdate: Starting update executor process...{Environment.NewLine}" +
                    $"ProcessStartInfo: {processStartInfo.FileName} {processStartInfo.Arguments}");

                var success = process.Start();
                this.logger.LogInformation($"StartUpdate: success={success}");
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
    }
}

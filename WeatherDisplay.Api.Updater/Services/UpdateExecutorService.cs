using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WeatherDisplay.Api.Updater.Models;

namespace WeatherDisplay.Api.Updater.Services
{
    internal class UpdateExecutorService : IUpdateExecutorService
    {
        private readonly HttpClient httpClient;
        private readonly IProcessFactory processFactory;

        public UpdateExecutorService(IProcessFactory processFactory)
        {
            this.processFactory = processFactory;
            this.httpClient = new HttpClient();
        }

        public async Task RunAsync(UpdateRequestDto updateRequestDto)
        {
            Log("Starting update process");

            foreach (var executorStep in updateRequestDto.ExecutorSteps)
            {
                Log($"Starting executor step '{executorStep.GetType().Name}'");

                // TODO: Implement factory to resolve executor steps
                try
                {

                    if (executorStep is DownloadFileStep downloadFileStep)
                    {
                        var downloadUrl = downloadFileStep.Url;
                        var downloadFilePath = Path.Combine(updateRequestDto.WorkingDirectory, downloadFileStep.DestinationFileName);
                        Log($"Starting file download {downloadUrl}");

                        var fileStream = await this.httpClient.GetStreamAsync(downloadUrl);
                        var writer = new StreamWriter(downloadFilePath);
                        await fileStream.CopyToAsync(writer.BaseStream);
                        writer.Close();

                        Log($"Download completed: {downloadFileStep.DestinationFileName}");
                    }
                    else if (executorStep is ExtractZipStep extractZipStep)
                    {
                        if (!File.Exists(extractZipStep.SourceArchiveFileName))
                        {
                            throw new FileNotFoundException(extractZipStep.SourceArchiveFileName);
                        }
                        else
                        {
                            Log($"Extracting zip file {extractZipStep.SourceArchiveFileName} to {extractZipStep.DestinationDirectoryName} (OverwriteFiles: {extractZipStep.OverwriteFiles})");

                            ZipFile.ExtractToDirectory(
                                extractZipStep.SourceArchiveFileName,
                                extractZipStep.DestinationDirectoryName,
                                extractZipStep.OverwriteFiles);

                            if (extractZipStep.DeleteSourceArchive)
                            {
                                Log($"Deleting zip file {extractZipStep.SourceArchiveFileName}");
                                File.Delete(extractZipStep.SourceArchiveFileName);
                            }
                        }
                    }
                    else if (executorStep is DeleteFileStep deleteFileStep)
                    {
                        File.Delete(deleteFileStep.Path);
                    }
                    else if (executorStep is ProcessStartExecutorStep processStartExecutorStep)
                    {
                        Log($"Starting process {processStartExecutorStep.FileName} {processStartExecutorStep.Arguments}");

                        var startInfo = new ProcessStartInfo
                        {
                            FileName = processStartExecutorStep.FileName,
                            Arguments = processStartExecutorStep.Arguments,
                            RedirectStandardOutput = processStartExecutorStep.RedirectStandardOutput,
                            UseShellExecute = processStartExecutorStep.UseShellExecute,
                            CreateNoWindow = processStartExecutorStep.CreateNoWindow,
                        };

                        var process = this.processFactory.CreateProcess(startInfo);
                        process.Start();
                        process.WaitForExit();

                        Log($"Process {processStartExecutorStep.FileName} {processStartExecutorStep.Arguments} finished with exit code {process.ExitCode}");
                    }
                    else
                    {
                        throw new NotSupportedException($"Executor step '{executorStep.GetType().Name}' is not supported");
                    }
                }
                catch (Exception ex)
                {
                    LogError(ex, $"Executor step '{executorStep.GetType().Name}' failed with exception");
                }
            }
        }

        private static void LogError(Exception exception, string message)
        {
            Log($"{message}|{exception.Message}{Environment.NewLine}{exception}");
        }

        private static void Log(string message)
        {
            Console.WriteLine(FormatLogMessage("UpdateExecutorService", message));
        }

        private static string FormatLogMessage(string tag, string message)
        {
            return $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.ffff}|{tag}|{message}";
        }
    }
}

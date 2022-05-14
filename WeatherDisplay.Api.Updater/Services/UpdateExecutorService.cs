using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WeatherDisplay.Api.Updater.Models;

namespace WeatherDisplay.Api.Updater.Services
{
    internal class UpdateExecutorService : IUpdateExecutorService
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<UpdateExecutorService> logger;
        private readonly IProcessFactory processFactory;

        public UpdateExecutorService(ILogger<UpdateExecutorService> logger, IProcessFactory processFactory)
        {
            this.logger = logger;
            this.httpClient = new HttpClient();
            this.processFactory = processFactory;
        }

        public async Task RunAsync(UpdateRequestDto updateRequestDto)
        {
            this.logger.LogInformation("RunAsync");

            var updateFile = Path.GetFileName(updateRequestDto.DownloadUrl);
            var downloadUrl = updateRequestDto.DownloadUrl;

            var fileStream = await this.httpClient.GetStreamAsync(downloadUrl);
            var writer = new StreamWriter(updateFile);
            await fileStream.CopyToAsync(writer.BaseStream);
            writer.Close();

            if (File.Exists(updateFile))
            {
                ZipFile.ExtractToDirectory(
                    sourceArchiveFileName: updateFile, 
                    destinationDirectoryName: updateRequestDto.WorkingDirectory,
                    overwriteFiles: true);
            }

            File.Delete(updateFile);

            foreach (var executorStep in updateRequestDto.ExecutorSteps)
            {
                this.logger.LogInformation($"ExecutorSteps: {executorStep.GetType().Name}");

                // TODO: Implement factory to resolve executor steps

                if (executorStep is DownloadFileStep downloadFileStep)
                {

                }
                else if (executorStep is ExtractZipStep extractZipStep)
                {

                }
                else if (executorStep is ProcessStartExecutorStep processStartExecutorStep)
                {
                    try
                    {
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
                    }
                    catch (Exception ex)
                    {
                        this.logger.LogError(ex, "Failed to run process");
                    }
                }

            }

        }
    }
}

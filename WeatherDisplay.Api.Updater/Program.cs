using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherDisplay.Api.Updater.Models;

namespace WeatherDisplay.Api.Updater
{
    internal class Program
    {
        private static async Task<int> Main(string[] args)
        {
            Console.WriteLine(
                $"WeatherDisplay.Api.Updater version {typeof(Program).Assembly.GetName().Version} {Environment.NewLine}" +
                $"Copyright(C) superdev GmbH. All rights reserved.{Environment.NewLine}");

            if (args.Length != 1)
            {
                Console.WriteLine(
                    $"Invalid arguments");

                return -1;
            }

            // TODO: Add logging and dependency injection

            var updateRequestJson = DecodeBase64(args[0]);
            Console.WriteLine("CommandLine: {0}", Environment.CommandLine);
            Console.WriteLine("args[0]: {0}", updateRequestJson);

            var updateRequestDto = JsonSerializer.Deserialize<UpdateRequestDto>(updateRequestJson.Replace("'", ""));
            Console.WriteLine("updateRequestDto.DownloadUrl: {0}", updateRequestDto.DownloadUrl);

            var currentDirectory = updateRequestDto.WorkingDirectory;
            Environment.CurrentDirectory = Path.GetFullPath(currentDirectory);

            var downloadUrl = updateRequestDto.DownloadUrl;
            await UpdateAsync(downloadUrl);

            return 0;
        }

        public static string DecodeBase64(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
            {
                return string.Empty;
            }

            var valueBytes = Convert.FromBase64String(base64String);
            return Encoding.UTF8.GetString(valueBytes);
        }

        private static async Task UpdateAsync(string downloadUrl)
        {
            var updateFile = "update.zip";

            var httpClient = new HttpClient();
            var fileStream = await httpClient.GetStreamAsync(downloadUrl);
            var writer = new StreamWriter(updateFile);
            await fileStream.CopyToAsync(writer.BaseStream);
            writer.Close();

            if (File.Exists(updateFile))
            {
                ZipFile.ExtractToDirectory(sourceArchiveFileName: updateFile, destinationDirectoryName: ".", overwriteFiles: true);
            }

            File.Delete(updateFile);

            // reboot
            try
            {
                var process = new Process()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "sudo",
                        Arguments = "reboot",
                        RedirectStandardOutput = false,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };
                process.Start();
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to reboot");
            }
        }
    }
}
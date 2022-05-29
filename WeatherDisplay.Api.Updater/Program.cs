using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherDisplay.Api.Updater.Models;
using WeatherDisplay.Api.Updater.Services;

namespace WeatherDisplay.Api.Updater
{
    internal class Program
    {
        private static async Task<int> Main(string[] args)
        {
#if DEBUG
            while (!Debugger.IsAttached)
            {
                Thread.Sleep(250);
            }
#endif
            if (args.Length == 0)
            {
                Console.WriteLine($"Invalid arguments");
                return -1;
            }

            var updateRequestJson = DecodeBase64(args[0]);
            var updateRequestDto = JsonConvert.DeserializeObject<UpdateRequestDto>(updateRequestJson.Replace("'", ""));

            var logFilePath = Path.Combine(updateRequestDto.WorkingDirectory, GetLogFileName());
            RedirectConsoleOutputToFile(logFilePath);

            Console.WriteLine(
                $"WeatherDisplay.Api.Updater version {typeof(Program).Assembly.GetName().Version} {Environment.NewLine}" +
                $"Copyright(C) superdev GmbH. All rights reserved.{Environment.NewLine}");

            Console.WriteLine($"WorkingDirectory={updateRequestDto.WorkingDirectory}");
            Console.WriteLine($"CallingProcessId={updateRequestDto.CallingProcessId}");
            Console.WriteLine();

            Environment.CurrentDirectory = Path.GetFullPath(updateRequestDto.WorkingDirectory);

            var updateExecutorService = new UpdateExecutorService(new SystemProcessFactory());
            await updateExecutorService.RunAsync(updateRequestDto);

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

        private static string GetLogFileName()
        {
            return $"update-log-{DateTime.Now:yyyyddMM-HHmmss-fff}.log";
        }

        private static void RedirectConsoleOutputToFile(string logFilename)
        {
            var filestream = new FileStream(logFilename, FileMode.Create);
            var streamwriter = new StreamWriter(filestream);
            streamwriter.AutoFlush = true;
            Console.SetOut(streamwriter);
            Console.SetError(streamwriter);
        }
    }
}
//using System.Diagnostics;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace RaspberryPi
//{
//    public static class Command
//    {
//        /// <summary>
//        /// Execute a process, wait for it to exit, and return the stdout/stderr output
//        /// </summary>
//        /// <param name="fileName">File to execute</param>
//        /// <param name="arguments">Command-line arguments</param>
//        /// <returns>Command output</returns>
//        public static async Task<string> Execute(string fileName, string arguments, CancellationToken cancellationToken = default)
//        {
//            var startInfo = new ProcessStartInfo
//            {
//                FileName = fileName,
//                Arguments = arguments
//            };

//            var output = new StringBuilder();
//            void OutputReceived(object sender, DataReceivedEventArgs e)
//            {
//                lock (output)
//                {
//                    output.Append(e.Data);
//                }
//            };
//            startInfo.RedirectStandardError = true;
//            startInfo.RedirectStandardOutput = true;

//            using var process = Process.Start(startInfo);
//            process.OutputDataReceived += OutputReceived;
//            process.ErrorDataReceived += OutputReceived;
//            process.BeginOutputReadLine();
//            process.BeginErrorReadLine();
//            await process.WaitForExitAsync(cancellationToken);

//            process.OutputDataReceived -= OutputReceived;
//            process.ErrorDataReceived -= OutputReceived;
//            return output.ToString();
//        }

//        /// <summary>
//        /// Execute a process to check if a condition is true or false
//        /// </summary>
//        /// <param name="fileName">File to execute</param>
//        /// <param name="arguments">Command-line arguments</param>
//        /// <returns>Query result</returns>
//        public static async Task<bool> ExecQuery(string fileName, string arguments, CancellationToken cancellationToken = default)
//        {
//            using var process = Process.Start(fileName, arguments);
//            await process.WaitForExitAsync(cancellationToken);
//            return process.ExitCode == 0;
//        }
//    }
//}
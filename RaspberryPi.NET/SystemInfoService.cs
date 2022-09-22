using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using RaspberryPi.Process;
using RaspberryPi.Storage;

namespace RaspberryPi
{
    public class SystemInfoService : ISystemInfoService
    {
        private const string CpuInfoFilePath = "/proc/cpuinfo";
        private const string MemInfoFilePath = "/proc/meminfo";
        private const string UptimeFilePath = "/proc/uptime";

        private readonly IFileSystem fileSystem;
        private readonly IProcessRunner processRunner;

        public SystemInfoService(
            IFileSystem fileSystem,
            IProcessRunner processRunner)
        {
            this.fileSystem = fileSystem;
            this.processRunner = processRunner;
        }

        public void SetHostname(string hostname)
        {
            if (string.IsNullOrEmpty(hostname))
            {
                throw new ArgumentException($"Parameter '{nameof(hostname)}' must not be null or empty", nameof(hostname));
            }

            this.processRunner.ExecuteCommand($"sudo hostnamectl set-hostname {hostname}");
        }

        public async Task<HostInfo> GetHostInfoAsync()
        {
            var hostInfo = new HostInfo();

            var commandLineResult = this.processRunner.ExecuteCommand("hostnamectl");

            using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(commandLineResult.OutputData));
            using var reader = new StreamReader(memoryStream);

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                line = line.Trim();

                if (CheckLineStartsWith(line, "Static hostname"))
                {
                    hostInfo.Hostname = ReadLineValue(line);
                }
                else if (CheckLineStartsWith(line, "Machine ID"))
                {
                    hostInfo.MachineId = ReadLineValue(line);
                }
                else if (CheckLineStartsWith(line, "Boot ID"))
                {
                    hostInfo.BootId = ReadLineValue(line);
                }
                else if (CheckLineStartsWith(line, "Operating System"))
                {
                    hostInfo.OperatingSystem = ReadLineValue(line);
                }
                else if (CheckLineStartsWith(line, "Kernel"))
                {
                    hostInfo.Kernel = ReadLineValue(line);
                }
                else if (CheckLineStartsWith(line, "Architecture"))
                {
                    hostInfo.Architecture = ReadLineValue(line);
                }
            }

            return hostInfo;
        }

        public async Task<CPUInfo> GetCPUInfoAsync()
        {
            var processorInfos = new List<ProcessorInfo>();
            var cpuInfo = new CPUInfo
            {
                Processors = processorInfos
            };

            var commandLineResult = this.processRunner.ExecuteCommand($"cat {CpuInfoFilePath}");

            using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(commandLineResult.OutputData));
            using var reader = new StreamReader(memoryStream);

            ProcessorInfo processorInfo = null;

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (CheckLineStartsWith(line, "processor"))
                {
                    processorInfo = new ProcessorInfo();
                }
                else if (line.Length <= 1)
                {
                    if (processorInfo != null)
                    {
                        processorInfos.Add(processorInfo);
                    }
                    processorInfo = null;
                }

                if (processorInfo != null)
                {
                    if (CheckLineStartsWith(line, "processor"))
                    {
                        processorInfo.Processor = ReadLineValue(line);
                    }
                    else if (CheckLineStartsWith(line, "model name"))
                    {
                        processorInfo.ModelName = ReadLineValue(line);
                    }
                }
                else
                {
                    if (CheckLineStartsWith(line, "Hardware"))
                    {
                        cpuInfo.Hardware = ReadLineValue(line);
                    }
                    else if (CheckLineStartsWith(line, "Revision"))
                    {
                        cpuInfo.Revision = ReadLineValue(line);
                    }
                    else if (CheckLineStartsWith(line, "Serial"))
                    {
                        cpuInfo.Serial = ReadLineValue(line);
                    }
                    else if (CheckLineStartsWith(line, "Model"))
                    {
                        cpuInfo.Model = ReadLineValue(line);
                    }
                }
            }

            return cpuInfo;
        }

        public int GetMemoryInfo()
        {
            var memInfoLines = this.fileSystem.File.ReadAllLines(MemInfoFilePath);

            foreach (var line in memInfoLines)
            {
                var lineParts = line.Split(new[] { ':' }, 2);
                if (lineParts.Length != 2)
                {
                    continue;
                }

                if (lineParts[0].ToLowerInvariant().Trim().Equals("memtotal") == false)
                {
                    continue;
                }

                var memKb = lineParts[1].ToLowerInvariant().Trim().Replace("kb", string.Empty).Trim();

                if (!int.TryParse(memKb, out var parsedMem))
                {
                    continue;
                }

                return parsedMem * 1024;
            }

            throw new InvalidOperationException("Failed to read current memory info");
        }

        private static bool CheckLineStartsWith(string line, string startsWith)
        {
            return line.StartsWith(startsWith, StringComparison.InvariantCultureIgnoreCase);
        }
        private static string ReadLineValue(string line)
        {
            return line.Substring(line.IndexOf(":") + 1).Trim();
        }
    }
}

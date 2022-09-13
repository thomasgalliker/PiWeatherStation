using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using RaspberryPi.Process;
using SystemProcess = System.Diagnostics.Process;

namespace RaspberryPi
{
    public class Journalctl : IJournalctl
    {
        private readonly string journalctlPath = "journalctl";
        private readonly IProcessRunner processRunner;

        public Journalctl(IProcessRunner processRunner)
        {
            this.processRunner = processRunner;
        }

        public List<string> GetLogs(string sysLogIdentifier)
        {
            // TODO: Use processRunner

            var process = SystemProcess.Start(new ProcessStartInfo
            {
                FileName = journalctlPath,
                Arguments = $"-u {sysLogIdentifier} -b",
                RedirectStandardOutput = true
            });
            TextReader reader = new StreamReader(process?.StandardOutput?.BaseStream);
            var contents = reader.ReadToEnd();
            return contents.Split('\n').ToList();
        }
    }
}
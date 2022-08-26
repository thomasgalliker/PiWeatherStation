using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace RaspberryPi
{
    public class Journalctl : IJournalctl
    {
        private readonly string journalctlPath = "journalctl";

        public Journalctl()
        {
        }

        public List<string> GetLogs(string sysLogIdentifier)
        {
            var process = Process.Start(new ProcessStartInfo
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
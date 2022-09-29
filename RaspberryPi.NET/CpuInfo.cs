using System.Collections.Generic;

namespace RaspberryPi
{
    public class CpuInfo
    {
        public CpuInfo()
        {
            this.Processors = new List<ProcessorInfo>();
        }

        public IReadOnlyCollection<ProcessorInfo> Processors { get; internal set; }

        public string Hardware { get; internal set; }
        
        public string Revision { get; internal set; }

        public string Serial { get; internal set; }
        
        public string Model { get; internal set; }
    }
}
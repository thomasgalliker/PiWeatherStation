namespace RaspberryPi
{
    public class HostInfo
    {
        public string Hostname { get; internal set; }

        public string MachineId { get; internal set; }

        public string BootId { get; internal set; }

        public string OperatingSystem { get; internal set; }

        public string Kernel { get; internal set; }

        public string Architecture { get; internal set; }
    }
}
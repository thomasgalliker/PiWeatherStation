using System.Threading.Tasks;

namespace RaspberryPi
{
    public interface ISystemInfoService
    {
        void SetHostname(string hostname);

        Task<CPUInfo> GetCPUInfoAsync();

        int GetMemoryInfo();

        Task<HostInfo> GetHostInfoAsync();
    }
}
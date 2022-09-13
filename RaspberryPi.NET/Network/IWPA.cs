using System.Collections.Generic;
using System.Threading.Tasks;

namespace RaspberryPi.Network
{
    public interface IWPA
    {
        Task<string> GetCountryCode();

        Task<List<string>> GetSSIDs();
        
        Task<string> GetReportAsync();

        Task Start();

        void Stop();

        Task UpdateSSIDAsync(string ssid, string psk, string countryCode);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RaspberryPi.Network
{
    public interface IWPA
    {
        Task<string> GetCountryCode();

        Task<List<string>> GetSSIDs();

        Task Start();

        void Stop();

        Task UpdateSSID(string ssid, string psk, string countryCode = null);
    }
}
﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherDisplay.Services
{
    public interface INetworkManager
    {
        IEnumerable<string> ScanAsync();

        Task<(string SSID, string PSK)> SetupAccessPointAsync();

        Task ConnectToWifiAsync(string ssid, string psk);

        IEnumerable<string> GetConnectedSSIDs();
    }
}

using System.Collections.Generic;

namespace RaspberryPi.Services
{
    public interface IServiceConfigurator
    {
        void InstallService(string serviceName, string execStart, string serviceDescription, string userName, IEnumerable<string> serviceDependencies);
       
        void ReinstallService(string serviceName, string execStart, string serviceDescription, string userName, IEnumerable<string> serviceDependencies);
        
        void UninstallService(string serviceName);
    }
}
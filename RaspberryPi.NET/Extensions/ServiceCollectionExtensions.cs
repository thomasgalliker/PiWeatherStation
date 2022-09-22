using Microsoft.Extensions.DependencyInjection;
using RaspberryPi.Network;
using RaspberryPi.Process;
using RaspberryPi.Services;
using RaspberryPi.Storage;

namespace RaspberryPi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRaspberryPi(this IServiceCollection services)
        {
            services.AddSingleton<IProcessRunner, ProcessRunner>();
            services.AddSingleton<IJournalctl, Journalctl>();

            // Network
            services.AddSingleton<IAccessPoint, AccessPoint>();
            services.AddSingleton<IDHCP, DHCP>();
            services.AddSingleton<IWPA, WPA>();
            services.AddSingleton<INetworkInterfaceService, NetworkInterfaceService>();

            // Services
            services.AddSingleton<IServiceConfigurator, LinuxServiceConfigurator>();
            services.AddSingleton<ISystemCtl, SystemCtl>();

            // Storage
            services.AddSingleton<IFileSystem, FileSystem>();

            // System
            services.AddSingleton<ISystemInfoService, SystemInfoService>();
        }
    }
}
using System.Collections.Generic;

namespace RaspberryPi.Services
{
    // TODO: This is the tip of the iceberg of very bad software design; let's refactor it.
    public class ServiceConfigurationState
    {
        public ServiceConfigurationState()
        {
            this.ServiceDependencies = new List<string>();
        }

        public bool Reconfigure { get; set; }

        public bool Install { get; set; }

        public bool Uninstall { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public ICollection<string> ServiceDependencies { get; set; }
    }
}
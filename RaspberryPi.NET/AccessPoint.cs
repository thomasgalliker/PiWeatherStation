//using System.IO;
//using System.Threading.Tasks;

//namespace RaspberryPi
//{
//    public class AccessPoint : IAccessPoint
//    {
//        public async Task<bool> IsEnabled()
//        {
//            return File.Exists("/etc/hostapd/wlan0.conf") && await Command.ExecQuery("systemctl", "is-active -q hostapd@wlan0.service");
//        }
//    }
//}
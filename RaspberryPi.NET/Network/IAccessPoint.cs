using System.Threading.Tasks;

namespace RaspberryPi.Network
{
    public interface IAccessPoint
    {
        Task<bool> IsEnabled();
    }
}
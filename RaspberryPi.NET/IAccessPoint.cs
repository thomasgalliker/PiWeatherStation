using System.Threading.Tasks;

namespace RaspberryPi
{
    public interface IAccessPoint
    {
        Task<bool> IsEnabled();
    }
}
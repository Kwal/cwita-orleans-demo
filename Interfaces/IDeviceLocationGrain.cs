using Orleans;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IDeviceLocationGrain : IGrainWithStringKey
    {
        Task Submit(double longitude, double latitude);
    }
}
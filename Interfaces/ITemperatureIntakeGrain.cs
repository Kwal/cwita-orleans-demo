using Orleans;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ITemperatureIntakeGrain : IGrainWithStringKey
    {
        Task Submit(decimal temp);
    }
}
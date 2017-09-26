using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ITemperatureAggregateGrain : IGrainWithStringKey
    {
        Task Update(string postalCode, IEnumerable<decimal> additionalTemps);

        Task<decimal> GetAverage(string postalCode);
    }
}
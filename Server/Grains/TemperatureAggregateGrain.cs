using Interfaces;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Server.Grains
{
    public class TemperatureAggregateGrain : Grain, ITemperatureAggregateGrain
    {
        private Dictionary<string, List<decimal>> _averages = new Dictionary<string, List<decimal>>(); 

        public Task<decimal> GetAverage(string postalCode)
        {
            var average = Decimal.Zero;
            if (_averages.TryGetValue(postalCode, out List<decimal> temps))
            {
                average = temps.Average();
            }

            return Task.FromResult(average);
        }

        public Task Update(string postalCode, IEnumerable<decimal> additionalTemps)
        {
            if (!_averages.TryGetValue(postalCode, out List<decimal> temps))
            {
                temps = new List<decimal>();
                _averages.Add(postalCode, temps);
            }

            temps.AddRange(additionalTemps);

            return Task.CompletedTask;
        }
    }
}
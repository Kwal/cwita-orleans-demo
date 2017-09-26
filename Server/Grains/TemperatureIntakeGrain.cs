using Interfaces;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Server.Grains
{
    public class TemperatureIntakeGrain : Grain, ITemperatureIntakeGrain
    {
        private List<decimal> _temps = new List<decimal>();

        public override Task OnActivateAsync()
        {
            RegisterTimer(SubmitTemps, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
            return base.OnActivateAsync();
        }

        public async Task Submit(decimal temp)
        {
            _temps.Add(temp);
            if (_temps.Count > 10)
            {
                await SubmitTemps(null);
            }
        }

        private async Task SubmitTemps(object state)
        {
            var keySegments = this.GetPrimaryKeyString().Split('|');
            var aggregateKey = DateTime.Parse(keySegments[1]).ToString("yyyy-MM-dd");

            var aggregateGrain = GrainFactory.GetGrain<ITemperatureAggregateGrain>(aggregateKey);
            await aggregateGrain.Update(keySegments[0], _temps);

            _temps.Clear();
        }
    }
}
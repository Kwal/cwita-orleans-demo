using Interfaces;
using Interfaces.Models;
using Orleans;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Grains
{
    public class SprocketWriteGrain : Grain, ISprocketWriteGrain
    {
        private HashRing<int> _ring;

        public override async Task OnActivateAsync()
        {
            _ring = new HashRing<int>(3, x => x);

            await base.OnActivateAsync();
        }

        public async Task Update(Sprocket sprocket)
        {
            var sprocketGrain = GrainFactory.GetGrain<ISprocketGrain>(Guid.Empty);
            await sprocketGrain.Update(sprocket);

            var sprockets = await sprocketGrain.GetAll();
            var replicaUpdates = _ring.Nodes
                .Select(x => GrainFactory.GetGrain<ISprocketReadReplicaGrain>(x).Sync(sprockets)
            );

            await Task.WhenAll(replicaUpdates);
        }
    }
}
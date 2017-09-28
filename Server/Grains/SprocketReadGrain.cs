using Interfaces;
using Interfaces.Models;
using Orleans;
using Orleans.Concurrency;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Grains
{
    [StatelessWorker]
    public class PatientSearchReader : Grain, ISprocketReadGrain
    {
        private HashRing<int> _ring;

        public override Task OnActivateAsync()
        {
            _ring = new HashRing<int>(3, x => x);

            return base.OnActivateAsync();
        }

        public Task<IEnumerable<Sprocket>> Search(string sessionId, string value)
        {
            var replicaId = _ring.GetNode(sessionId);
            Log.Logger.Information("Performing sprocket search using replica {0}", replicaId);

            var replica = GrainFactory.GetGrain<ISprocketReadReplicaGrain>(replicaId);
            return replica.Search(value);
        }
    }
}
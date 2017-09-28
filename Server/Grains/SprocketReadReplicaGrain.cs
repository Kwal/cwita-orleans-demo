using Interfaces;
using Interfaces.Models;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Grains
{
    public class SprocketReadReplicaGrain : Grain, ISprocketReadReplicaGrain
    {
        private List<Sprocket> _sprockets = new List<Sprocket>();

        public Task<IEnumerable<Sprocket>> Search(string value)
        {
            var matches = _sprockets
                .Where(x => x.Name.IndexOf(value, StringComparison.InvariantCultureIgnoreCase) > -1)
                .ToList();

            return Task.FromResult(matches.AsEnumerable());
        }

        public Task Sync(IEnumerable<Sprocket> sprockets)
        {
            _sprockets = sprockets.ToList();

            return Task.CompletedTask;
        }
    }
}
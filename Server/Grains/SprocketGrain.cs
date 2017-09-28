using Interfaces;
using Interfaces.Models;
using Orleans;
using Orleans.Providers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Grains
{
    public class SprocketGrain : Grain, ISprocketGrain
    {
        private Dictionary<int, Sprocket> _sprockets = new Dictionary<int, Sprocket>();

        public Task<IEnumerable<Sprocket>> GetAll()
        {
            return Task.FromResult(_sprockets.Values.ToList().AsEnumerable());
        }

        public Task Update(Sprocket sprocket)
        {
            _sprockets[sprocket.Id] = sprocket;

            return Task.CompletedTask;
        }
    }
}
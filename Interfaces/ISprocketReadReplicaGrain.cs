using Interfaces.Models;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ISprocketReadReplicaGrain : IGrainWithIntegerKey
    {
        Task Sync(IEnumerable<Sprocket> sprockets);

        Task<IEnumerable<Sprocket>> Search(string value);
    }
}
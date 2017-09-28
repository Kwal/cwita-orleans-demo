using Interfaces.Models;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ISprocketGrain : IGrainWithGuidKey
    {
        Task Update(Sprocket sprocket);

        Task<IEnumerable<Sprocket>> GetAll();
    }
}
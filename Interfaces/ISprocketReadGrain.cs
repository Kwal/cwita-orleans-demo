using Interfaces.Models;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ISprocketReadGrain : IGrainWithGuidKey
    {
        Task<IEnumerable<Sprocket>> Search(string sessionId, string value);
    }
}
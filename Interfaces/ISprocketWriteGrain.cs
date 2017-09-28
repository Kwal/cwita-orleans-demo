using Interfaces.Models;
using Orleans;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ISprocketWriteGrain : IGrainWithGuidKey
    {
        Task Update(Sprocket sprocket);
    }
}
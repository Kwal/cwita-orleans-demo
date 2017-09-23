using Orleans;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IHelloWorldGrain : IGrainWithIntegerKey
    {
        Task<string> SayHello(string name);
    }
}
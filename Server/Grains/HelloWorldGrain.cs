using System.Threading.Tasks;
using Interfaces;
using Orleans;

namespace Server
{
    public class HelloWorldGrain : Grain, IHelloWorldGrain
    {
        public Task<string> SayHello(string name)
        {
            return Task.FromResult($"Oh hai {name}");
        }
    }
}
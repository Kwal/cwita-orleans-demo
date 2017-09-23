using System.Threading.Tasks;
using Interfaces;
using Orleans;

namespace Server
{
    public class HelloWorldGrain : Grain, IHelloWorldGrain
    {
        public async Task<string> SayHello(string name)
        {
            await Task.Delay(2000);
            
            return $"Oh hai {name}";
        }
    }
}
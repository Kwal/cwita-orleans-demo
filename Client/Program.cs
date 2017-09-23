using Interfaces;
using Orleans;
using Orleans.Runtime;
using Orleans.Runtime.Configuration;
using Polly;
using System;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = ClientConfiguration.LocalhostSilo();
            config.DefaultTraceLevel = Severity.Warning;
            config.TraceFileName = null;
            config.TraceToConsole = false;

            var client = new ClientBuilder().UseConfiguration(config).Build();
            await client.Connect();

            var helloWorldGrain = client.GetGrain<IHelloWorldGrain>(0);
            var message = await helloWorldGrain.SayHello("Matt");

            Console.WriteLine(message);
            Console.ReadKey(true);
        }
    }
}

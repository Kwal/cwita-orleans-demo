using Interfaces;
using Orleans;
using Orleans.Runtime;
using Orleans.Runtime.Configuration;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.LiterateConsole()
                .CreateLogger();

            var config = ClientConfiguration.LocalhostSilo();
            config.DefaultTraceLevel = Severity.Warning;
            config.TraceFileName = null;
            config.TraceToConsole = false;

            using (var client = new ClientBuilder().UseConfiguration(config).Build())
            {
                await client.Connect();

                await SayHello(client, "Matt");

                await Task.WhenAll(
                    SayHello(client, "Alice"),
                    SayHello(client, "Bob")
                );

                Console.ReadKey(true);
            }
        }

        static async Task SayHello(IClusterClient client, string name)
        {
            var helloWorldGrain = client.GetGrain<IHelloWorldGrain>(1);
            var message = await helloWorldGrain.SayHello(name);

            Log.Logger.Information(message);
        }
    }
}

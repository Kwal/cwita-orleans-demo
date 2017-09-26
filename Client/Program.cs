using Interfaces;
using Orleans;
using Orleans.Runtime;
using Orleans.Runtime.Configuration;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                .CreateLogger();

            var config = ClientConfiguration.LocalhostSilo();
            config.DefaultTraceLevel = Severity.Warning;
            config.TraceFileName = null;
            config.TraceToConsole = false;

            using (var client = new ClientBuilder().UseConfiguration(config).Build())
            {
                await client.Connect();

                async Task sayHello(string name)
                {
                    var helloWorldGrain = client.GetGrain<IHelloWorldGrain>(1);
                    var message = await helloWorldGrain.SayHello(name);

                    Log.Logger.Information(message);
                };

                await sayHello("Matt");

                // await Task.WhenAll(
                //     SayHello(client, "Alice"),
                //     SayHello(client, "Bob")
                // );


                // var widgetId = new Random().Next(0, 100);

                // Log.Logger.Information("Let's grab a widget...");
                // var widgetGrain = client.GetGrain<IWidgetGrain>(widgetId);
                // var widget = await widgetGrain.Get();
                // Log.Logger.Information("{@Widget}", widget);

                // Log.Logger.Information("Let's grab that widget again...");
                // widget = await widgetGrain.Get();
                // Log.Logger.Information("{@Widget}", widget);

                Console.ReadKey(true);
            }
        }
    }
}
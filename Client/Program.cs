using Interfaces;
using Orleans;
using Orleans.Runtime;
using Orleans.Runtime.Configuration;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Threading;
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

                var deviceIds = new[] { "123", "456", "789" };
                using (var cancellation = new CancellationTokenSource())
                {
                    var token = cancellation.Token;
                    var task = Task.Run(async () =>
                    {
                        var rand = new Random();
                        while (!token.IsCancellationRequested)
                        {
                            var randomDevice = deviceIds[rand.Next(0, deviceIds.Length)];
                            var intakeGrain = client.GetGrain<IDeviceLocationGrain>(randomDevice);
                            await intakeGrain.Submit(rand.NextDouble(), rand.NextDouble());

                            await Task.Delay(250);
                        }
                    }, token);

                    Log.Logger.Information("Press any key to stop location updates...");
                    Console.ReadKey(true);
                    cancellation.Cancel();
                }

                var devicesGrain = client.GetGrain<IDevicesGrain>(0);
                foreach (var id in deviceIds)
                {
                    var location = await devicesGrain.GetLocation(id);
                    Log.Logger.Information("{Device} location: {Location}", id, location);
                }

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
using Bogus;
using Interfaces;
using Interfaces.Models;
using Orleans;
using Orleans.Runtime;
using Orleans.Runtime.Configuration;
using Serilog;
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
                .WriteTo.Console()
                .CreateLogger();

            var config = ClientConfiguration.LocalhostSilo();
            config.DefaultTraceLevel = Severity.Warning;
            config.TraceFileName = null;
            config.TraceToConsole = false;

            using (var client = new ClientBuilder().UseConfiguration(config).Build())
            {
                await client.Connect();


                Console.WriteLine("Press any key to exit...");
                Console.ReadKey(true);
            }
        }
    }
}



// var helloWorldGrain = client.GetGrain<IHelloWorldGrain>(1);
// var message = await helloWorldGrain.SayHello("Matt");

// Log.Logger.Information(message);




// async Task sayHello(string name)
// {
//     var helloWorldGrain = client.GetGrain<IHelloWorldGrain>(1);
//     var message = await helloWorldGrain.SayHello(name);

//     Log.Logger.Information(message);
// };

// await Task.WhenAll(
//     sayHello("Alice"),
//     sayHello("Bob")
// );



// var widgetId = new Random().Next(0, 100);
// var widgetGrain = client.GetGrain<IWidgetGrain>(widgetId);

// Log.Logger.Information("Let's grab widget {WidgetId}...", widgetId);
// var widget = await widgetGrain.Get();
// Log.Logger.Information("{@Widget}", widget);

// Log.Logger.Information("Let's update widget {WidgetId}...", widgetId);
// await widgetGrain.Set(
//     new Widget { Id = widgetId, Sku = "ABC123" }
// );

// Log.Logger.Information("Let's grab widget {WidgetId} again...", widgetId);
// widget = await widgetGrain.Get();
// Log.Logger.Information("{@Widget}", widget);




// var deviceIds = new[] { "123", "456", "789" };
// using (var cancellation = new CancellationTokenSource())
// {
//     var token = cancellation.Token;
//     var task = Task.Run(async () =>
//     {
//         var rand = new Random();
//         while (!token.IsCancellationRequested)
//         {
//             var randomDevice = deviceIds[rand.Next(0, deviceIds.Length)];
//             var intakeGrain = client.GetGrain<IDeviceLocationGrain>(randomDevice);
//             await intakeGrain.Submit(rand.NextDouble(), rand.NextDouble());

//             await Task.Delay(250);
//         }
//     }, token);

//     Log.Logger.Information("Press any key to stop location updates...");
//     Console.ReadKey(true);
//     cancellation.Cancel();
// }

// await Task.Delay(5000); // wait for last result to come in

// var devicesGrain = client.GetGrain<IDevicesGrain>(0);
// foreach (var id in deviceIds)
// {
//     var location = await devicesGrain.GetLocation(id);
//     Log.Logger.Information("{Device} location: {Location}", id, location);
// }




// var generator = new Faker<Sprocket>()
//     .RuleFor(x => x.Id, x => x.IndexGlobal)
//     .RuleFor(x => x.Name, x => x.Random.Word());

// var sprockets = generator.Generate(20);
// var sprocketWriter = client.GetGrain<ISprocketWriteGrain>(Guid.Empty);
// foreach (var s in sprockets)
// {
//     await sprocketWriter.Update(s);
// }

// var rand = new Random();
// var sprocketReader = client.GetGrain<ISprocketReadGrain>(Guid.Empty);
// for (var i = 0; i < 10; i++)
// {
//     var searchValue = sprockets[rand.Next(0, sprockets.Count)].Name.Substring(0, 3);
//     var results = await sprocketReader.Search(Guid.NewGuid().ToString(), searchValue);
//     if (results != null)
//     {
//         Log.Logger.Information("Found {@Sprockets}", results);
//     }
// }
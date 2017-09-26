using Orleans.Runtime;
using Orleans.Runtime.Configuration;
using Orleans.Runtime.Host;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System;

namespace actors
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                .CreateLogger();

            var config = ClusterConfiguration.LocalhostPrimarySilo();
            config.AddMemoryStorageProvider();
            config.Defaults.DefaultTraceLevel = Severity.Warning;
            config.Defaults.TraceFileName = null;
            config.Defaults.TraceFilePattern = null;
            config.Defaults.TraceToConsole = false;
            config.Defaults.StatisticsCollectionLevel = StatisticsLevel.Critical;

            using (var host = new SiloHost("Demo", config))
            {
                host.InitializeOrleansSilo();
                host.StartOrleansSilo();

                Console.WriteLine("Press any key to stop silo...");
                Console.ReadKey(true);

                host.StopOrleansSilo();
            }
        }
    }
}

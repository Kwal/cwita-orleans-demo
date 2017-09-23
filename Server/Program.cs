using Orleans.Runtime;
using Orleans.Runtime.Configuration;
using Orleans.Runtime.Host;
using System;

namespace actors
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ClusterConfiguration.LocalhostPrimarySilo();
            config.AddMemoryStorageProvider();
            config.Defaults.DefaultTraceLevel = Severity.Warning;
            config.Defaults.TraceFileName = null;
            config.Defaults.TraceToConsole = false;

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

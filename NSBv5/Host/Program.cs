using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Persistence;

namespace Host
{
    class Program
    {
        static ILog log = LogManager.GetLogger<Program>();

        static void Main(string[] args)
        {
            var busConfiguration = new BusConfiguration();
            busConfiguration.EndpointName("MultipleSagas.Host");
            busConfiguration.UseSerialization<JsonSerializer>();
            busConfiguration.EnableInstallers();

            var persistence = busConfiguration.UsePersistence<NHibernatePersistence>();
            persistence.ConnectionString(@"Data Source=.;Initial Catalog=MultipleSagas;Integrated Security=True");


            using (var bus = Bus.Create(busConfiguration).Start())
            {
                do
                {
                    while (!Console.KeyAvailable)
                    {
                        log.InfoFormat("Sending new scrapped Data");
                        bus.SendLocal(new NewScrappedDataCommand { ProductId = Guid.NewGuid() });
                        Thread.Sleep(4000); // 20,000 a day equates to about one every 4 seconds
                    }
                } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            }
        }
    }
}

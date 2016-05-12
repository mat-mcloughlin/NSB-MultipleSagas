using System;
using NServiceBus;

namespace Dispatcher2
{
    class Program
    {
        static void Main(string[] args)
        {
            var busConfiguration = new BusConfiguration();
            busConfiguration.EndpointName("Timeout.Dispatcher");
            busConfiguration.UseSerialization<JsonSerializer>();
            busConfiguration.EnableInstallers();
            busConfiguration.UsePersistence<InMemoryPersistence>();

            Bus.Create(busConfiguration).Start();
            Console.ReadKey();
        }
    }
}

using System.Threading.Tasks;
using Messages;
using NServiceBus;

namespace Host.Handlers
{
    public class GatherProductHandler : IHandleMessages<GatherProductCommand>
    {
        readonly IBus _bus;

        public GatherProductHandler(IBus bus)
        {
            _bus = bus;
        }
        public void Handle(GatherProductCommand message)
        {
            // This process retrieves data by screenscraping and storing it in the database
            _bus.Reply(new ProductGatheredMessage());
        }
    }
}

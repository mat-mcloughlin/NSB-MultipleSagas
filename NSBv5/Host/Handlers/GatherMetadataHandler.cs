using System.Threading.Tasks;
using Messages;
using NServiceBus;

namespace Host.Handlers
{
    public class GatherMetadataHandler : IHandleMessages<GatherMetadataCommand>
    {
        readonly IBus _bus;

        public GatherMetadataHandler(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(GatherMetadataCommand message)
        {
            _bus.Reply(new MetadataGatheredMessage());
        }
    }
}

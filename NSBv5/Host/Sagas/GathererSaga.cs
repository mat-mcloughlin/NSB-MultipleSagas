using System;
using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Saga;
using NServiceBus.Unicast;

namespace Host.Sagas
{
    public class GathererSaga : Saga<GathererData>,
        IAmStartedByMessages<NewScrappedDataCommand>,
        IHandleMessages<ProductGatheredMessage>,
        IHandleMessages<MetadataGatheredMessage>,
        IHandleTimeouts<GatherTimeout>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<GathererData> mapper)
        {
            mapper.ConfigureMapping<NewScrappedDataCommand>(message => message.ProductId).ToSaga(sagaData => sagaData.ProductId);
        }

        public void Handle(NewScrappedDataCommand message)
        {
            Data.ProductId = message.ProductId;
            Bus.SendLocal(new GatherProductCommand { ProductId = message.ProductId });
            Bus.SendLocal(new GatherMetadataCommand { ProductId = message.ProductId });
            RequestTimeout<GatherTimeout>(TimeSpan.FromMinutes(1));
        }

        public void Handle(ProductGatheredMessage message)
        {
            Data.ProductGathered = true;
            VerifyIfComplete();
        }

        public void Handle(MetadataGatheredMessage message)
        {
            Data.MetadataGathered = true;
            VerifyIfComplete();
        }

        private void VerifyIfComplete()
        {
            if (Data.ProductGathered && Data.MetadataGathered)
            {
                Bus.Publish<GatheringDataCompletedEvent>(e => { e.ProductId = Data.ProductId; });
                MarkAsComplete();
            }
        }

        public void Timeout(GatherTimeout state)
        {
            // If for some reason it took too long to find the information then we might need to notify somebody
        }
    }
}

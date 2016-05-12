using System;
using NServiceBus.Saga;

namespace Host.Sagas
{
    public class GathererData : IContainSagaData
    {
        public virtual Guid Id { get; set; }
        public virtual string Originator { get; set; }
        public virtual string OriginalMessageId { get; set; }
        public virtual Guid ProductId { get; set; }
        public virtual bool ProductGathered { get; set; }
        public virtual bool MetadataGathered { get; set; }
    }
}
using System;
using NServiceBus;

namespace Messages
{
    public class GatheringDataCompletedEvent : IEvent
    {
        public Guid ProductId { get; set; }
    }
}
using System;
using NServiceBus;

namespace Messages
{
    public class GatherMetadataCommand : ICommand
    {
        public Guid ProductId { get; set; }
    }
}
using System;
using NServiceBus;

namespace Messages
{
    public class GatherProductCommand : ICommand
    {
        public Guid ProductId { get; set; }
    }
}
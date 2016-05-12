using System;
using NServiceBus;

namespace Messages
{
    public class NewScrappedDataCommand : ICommand
    {
        public Guid ProductId { get; set; }
    }
}
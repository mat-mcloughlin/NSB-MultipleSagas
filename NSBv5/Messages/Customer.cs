using System;

namespace Messages
{
    public class Customer
    {
        public Guid Id { get; set; }
        public DispatchMethod DispatchMethod { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace Messages
{
    public static class Customers
    {
        static readonly Random Rand = new Random();

        public static List<Customer> List = new List<Customer>();

        
        static Customers()
        {
            for (var i = 0; i < 20; i++)
            {
                var dispatchMethod = Rand.Next(2) == 1 ? DispatchMethod.Immediately : DispatchMethod.NigthlyBatch;
                var deliveryMethod = Rand.Next(2) == 1 ? DeliveryMethod.Ftp : DeliveryMethod.Http;
                List.Add(new Customer { Id = Guid.NewGuid(), DispatchMethod = dispatchMethod, DeliveryMethod = deliveryMethod });
            }
        }
    }
}

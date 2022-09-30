using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.RabbitMQ.Gateway.Models.Offer
{
    public class OfferReadModel
    {
        public int Id{ get; set; } 
        public string? Name{ get; set; }
        public string? Publisher { get; set; }
        public string? Amount { get; set; }

    }
}

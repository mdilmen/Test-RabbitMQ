using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.RabbitMQ.Gateway.Models.Offer
{
    public class OfferRequest 
    {
        public string? Company { get; set; }
        public string? Product { get; set; }
        public int BrickId { get; set; }
        public string? RequestData { get; set; }
    }
}

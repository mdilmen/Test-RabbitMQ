using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Test.RabbitMQ.Gateway.Models.Offer.Enums;

namespace Test.RabbitMQ.OfferService.Models.Offer
{
    public class SYHRequest : RequestBase
    {
        public string? Country { get; set; }
        public DateTime TripStart { get; set; }
        public DateTime TripEnd { get; set; }
        public TransportationType TransportationType { get; set; }
    }
}

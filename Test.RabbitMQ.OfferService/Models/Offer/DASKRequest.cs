using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.RabbitMQ.OfferService.Models.Offer;

namespace OGS.OfferService.Api.Models.ProductRequests
{
    public class DASKRequest : RequestBase
    {
        public string? UAVT { get; set; }
    }
}

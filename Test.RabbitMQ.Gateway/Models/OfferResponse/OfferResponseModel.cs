using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.RabbitMQ.Gateway.Models.OfferResponse
{
    public class OfferResponseModel
    {
        public Guid OfferGuid { get; set; }
        public int BrickId { get; set; }
        public string? OfferData { get; set; }
        public decimal Premium { get; set; }
        public string? OfferNumber { get; set; }
        public string? CompanyName { get; set; }
    }
}

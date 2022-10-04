using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.RabbitMQ.Gateway.Models.Offer.Enums;

namespace Test.RabbitMQ.OfferService.Models.Offer
{
    public interface IRequestModel
    {
        public string? TCKN { get; set; }
        public DateTime BirthDate { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Gsm { get; set; }
        public OfferType OfferType { get; set; }
        public int BrickId { get; set; }
    }
}

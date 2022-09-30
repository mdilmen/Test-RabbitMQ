using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.RabbitMQ.Gateway.Models.Offer
{
    public class OfferCreateModel
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Publisher { get; set; }
        [Required]
        public string? Amount { get; set; }

    }
}

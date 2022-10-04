using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.RabbitMQ.Gateway.Models.Offer;

namespace Test.RabbitMQ.Gateway.AsyncDataServices
{
    public interface IMessageBusClient
    {
        Task PublishNewOffer(OfferRequest model);
    }
}

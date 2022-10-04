using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.RabbitMQ.OfferService.Models.Offer;
using Test.RabbitMQ.OfferService.Models.OfferResponse;

namespace Test.RabbitMQ.OfferService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        Task PublishNewOfferResponse(OfferResponseModel model);
    }
}

using AutoMapper;
using Newtonsoft.Json;
using OGS.OfferService.Api.Models.ProductRequests;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.RabbitMQ.Offer.Models.Offer;
using Test.RabbitMQ.OfferService.AsyncDataServices;
using Test.RabbitMQ.OfferService.Models.Offer;
using Test.RabbitMQ.OfferService.Models.OfferResponse;

namespace Test.RabbitMQ.OfferService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;
        private readonly IMessageBusClient _busClient;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper, IMessageBusClient busClient)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
            _busClient = busClient;
        }
        public async Task ProcessEvent(string message)
        {
            var offerResponseModel = new OfferResponseModel();
            var offer = JsonConvert.DeserializeObject<OfferRequest>(message);
            var company = offer.Company;
            var product = offer.Product;

            // to the repository..
            if (offer != null)
            {
                if (company.Equals("AXA") && product.Equals("CEP"))
                {
                    var cepRequest = JsonConvert.DeserializeObject<CEPRequest>(offer.RequestData);
                    // Do the get offer stuff..
                    // Return the response 

                    offerResponseModel.BrickId = offer.BrickId;
                    offerResponseModel.CompanyName = offer.Company;
                    offerResponseModel.OfferNumber = "123412312";
                    offerResponseModel.Premium = 769;

                    var result = JsonConvert.SerializeObject(offerResponseModel);
                    Log.Information($" returning OfferResponse : {result}");
                }
            }

            // TO THE QUEU
            if (offerResponseModel.BrickId > 0)
            {
                await _busClient.PublishNewOfferResponse(offerResponseModel);
            }            
            //return Task.CompletedTask;
        }
    }
}

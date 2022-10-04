using AutoMapper;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.RabbitMQ.Gateway.Models.Offer;
using Test.RabbitMQ.Gateway.Models.Offer.Enums;
using Test.RabbitMQ.Gateway.Models.OfferResponse;

namespace Test.RabbitMQ.Gateway.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }
        public Task ProcessEvent(string message)
        {
            var result = JsonConvert.DeserializeObject<OfferResponseModel>(message);

            // Write Down to Repository..
            Log.Information($"{message}");

            return Task.CompletedTask;
        }
    }
}

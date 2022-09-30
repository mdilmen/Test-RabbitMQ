using AutoMapper;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.RabbitMQ.Gateway.Models.Offer;

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
            var eventType = DetermineEvent(message);

            return Task.CompletedTask;
            //throw new NotImplementedException();
        }
        private Task addOffer(string offerPublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {

                var model = JsonConvert.DeserializeObject<OfferPublishedModel>(offerPublishedMessage);
                Log.Information($"Offer Received from Que {model}");            
            }

            return Task.CompletedTask;
        
        }
        private EventType DetermineEvent(string notificationMessage)
        {
            try
            {
                Log.Information($"Determine Message {notificationMessage}");
                var eventType = JsonConvert.DeserializeObject<EventType>(notificationMessage);

                return eventType;
            }
            catch (Exception)
            {

                return EventType.OfferPublished;
            }
        }
    }
    enum EventType
    { 
        OfferPublished,
        OfferUnpublished,    
    }
}

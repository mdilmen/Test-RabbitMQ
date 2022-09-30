using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.RabbitMQ.Gateway.AsyncDataServices;
using Test.RabbitMQ.Gateway.Models.Offer;

namespace Test.RabbitMQ.Gateway.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IMessageBusClient _messageBus;
        private readonly IMapper _mapper;

        public OfferController(IMessageBusClient messageBus, IMapper mapper)
        {
            _messageBus = messageBus;
            _mapper = mapper;
        }
        [HttpPost]
        public IActionResult SendOffer([FromBody] OfferModel model)
        {
            try
            {
                model.Id = new Random().Next(1,10000);
                var offerPublishedModel = _mapper.Map<OfferPublishedModel>(model);
                offerPublishedModel.Event = "Offer Published";                
                _messageBus.PublishNewOffer(offerPublishedModel);
                return Ok($"Offer Published : {model.Name}");
            }
            catch (Exception ex)
            {
                return Ok($"Offer NOT Published : {ex.Message}");                
            }

        }
    }
}

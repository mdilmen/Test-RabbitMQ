using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.RabbitMQ.OfferService.AsyncDataServices;
using Test.RabbitMQ.OfferService.Models.Offer;
using Test.RabbitMQ.OfferService.Models.OfferResponse;

namespace Test.RabbitMQ.OfferService.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class OfferResponseController : ControllerBase
    {
        private readonly IMessageBusClient _messageBus;
        private readonly IMapper _mapper;

        public OfferResponseController(IMessageBusClient messageBus, IMapper mapper)
        {
            _messageBus = messageBus;
            _mapper = mapper;
        }
        [HttpPost]
        public IActionResult SendOfferResponse([FromBody] OfferResponseModel model)
        {
            try
            {

                model.Event = "Offer Response Published";
                _messageBus.PublishNewOfferResponse(model);
                return Ok($"Offer Published : {model.OfferGuid}");
            }
            catch (Exception ex)
            {
                return Ok($"Offer NOT Published : {ex.Message}");
            }

        }
    }
}

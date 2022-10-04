using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public IActionResult SendOffer([FromBody] OfferRequest model)
        {
            model = ManipulateOfferRequest(model);
            try
            {
                _messageBus.PublishNewOffer(model);
                return Ok($"Offer Published for Brick : {model.BrickId}");
            }
            catch (Exception ex)
            {
                return Ok($"Offer NOT Published : {ex.Message}");
            }

        }
        private static OfferRequest ManipulateOfferRequest(OfferRequest model)
        {
            var cepRequest = new CepRequest();
            model.RequestData = JsonConvert.SerializeObject(cepRequest);
            return model;
        }

    }
    public class CepRequest
    {
        public CepRequest()
        {

        }
        public string? IMEI { get; set; } = "2349234992349";
        public string? TCKN { get; set; } = "2701272828";
        public string? FullName { get; set; } = "Ad Soyad";
        public string? GSM { get; set; } = "05437585757";
        public string? Email { get; set; } = "mmdilmen@gmail.com";

    }

}

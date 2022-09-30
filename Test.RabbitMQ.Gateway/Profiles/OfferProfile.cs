using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.RabbitMQ.Gateway.Models.Offer;

namespace Test.RabbitMQ.Gateway.Profiles
{
    public class OfferProfile : Profile
    {
        public OfferProfile()
        {
            CreateMap<OfferModel, OfferPublishedModel>().ReverseMap();
        }
    }
}

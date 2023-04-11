using AutoMapper;
using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Shop.Application.DTOs.Customer;
using Homework.Enigmatry.Shop.Application.DTOs.Order;
using Homework.Enigmatry.Shop.Domain.Entities;

namespace Homework.Enigmatry.Shop.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ArticleDetailsDto, Article>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<Order, OrderDto>() .ReverseMap();


        }
    }
}

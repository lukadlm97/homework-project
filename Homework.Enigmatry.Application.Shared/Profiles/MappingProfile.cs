using AutoMapper;
using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Shop.Domain.Entities;

namespace Homework.Enigmatry.Application.Shared.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Article, ArticleDto>().ReverseMap();
            
        }
    }
}

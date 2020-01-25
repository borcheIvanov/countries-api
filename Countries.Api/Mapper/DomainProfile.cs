using AutoMapper;
using Countries.Api.DTModels;
using Countries.Api.Models;

namespace Countries.Api.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Country, CountryResponseModel>();
            CreateMap<Country, CountryDetailsResponseModel>();
        }
    }
}
using AutoMapper;
using Data.Core.Domain.Entities;
using WebAPI.Models.CityModels;

namespace WebAPI.Mappings
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, DisplayCityModel>().ReverseMap();
        }
    }
}

using AutoMapper;
using Data.Core.Domain.Entities;
using WebAPI.Models.CountryModels;

namespace WebAPI.Mappings
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<Country, DisplayCountryModel>().ReverseMap();
        }
    }
}

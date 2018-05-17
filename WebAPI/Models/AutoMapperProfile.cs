using AutoMapper;
using Data.Core.Domain;
using WebAPI.Models.AddressModels;
using WebAPI.Models.CityModels;
using WebAPI.Models.CountryModels;
using WebAPI.Models.UserModels;

namespace WebAPI.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User mapping
            CreateMap<User, CreatingUserModel>().ReverseMap();
            CreateMap<User, DisplayUserModel>().ReverseMap();

            // Address mapping
            CreateMap<Address, DisplayAddressModel>().ReverseMap();

            // City mapping
            CreateMap<City, DisplayCityModel>().ReverseMap();

            // Country mapping
            CreateMap<Country, DisplayCountryModel>().ReverseMap();
        }
    }
}

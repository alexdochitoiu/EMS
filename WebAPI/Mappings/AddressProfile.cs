using AutoMapper;
using Data.Core.Domain.Entities;
using WebAPI.Models.AddressModels;

namespace WebAPI.Mappings
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, DisplayAddressModel>().ReverseMap();
        }
    }
}

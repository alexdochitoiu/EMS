using AutoMapper;
using Data.Core.Domain.Entities;
using WebAPI.Models.UserModels;

namespace WebAPI.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, CreatingUserModel>().ReverseMap();
            CreateMap<User, DisplayUserModel>().ReverseMap();
        }
    }
}

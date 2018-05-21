using AutoMapper;
using Data.Core.Domain.Entities.Identity;
using WebAPI.Models.UserModels;

namespace WebAPI.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, CreatingUserModel>().ReverseMap();
            CreateMap<ApplicationUser, DisplayUserModel>().ReverseMap();
        }
    }
}

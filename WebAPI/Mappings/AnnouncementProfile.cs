using AutoMapper;
using Data.Core.Domain.Entities;
using WebAPI.Models.AnnouncementModels;

namespace WebAPI.Mappings
{
    public class AnnouncementProfile : Profile
    {
        public AnnouncementProfile()
        {
            CreateMap<Announcement, DisplayAnnouncementModel>()
                .ForMember(d => d.Created,
                    expression => expression.ResolveUsing(s => s.Created.ToString("dddd, dd MMMM yyyy HH:mm")))
                .ForMember(d => d.Id,
                    expression => expression.ResolveUsing(s => s.Id.ToString()))
                .ReverseMap();

            CreateMap<Announcement, CreatingAnnouncementModel>()
                .ReverseMap();
        }
    }
}

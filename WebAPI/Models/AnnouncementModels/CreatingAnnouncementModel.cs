using System;
using Data.Core.Domain;

namespace WebAPI.Models.AnnouncementModels
{
    public class CreatingAnnouncementModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public SeverityEnum Severity { get; set; }
        public Guid UserId { get; set; }
    }
}

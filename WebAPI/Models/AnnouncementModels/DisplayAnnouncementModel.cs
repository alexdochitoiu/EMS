using Data.Core.Domain;
using WebAPI.Models.UserModels;

namespace WebAPI.Models.AnnouncementModels
{
    public class DisplayAnnouncementModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public SeverityEnum Severity { get; set; }
        public string Created { get; set; }
        public DisplayUserModel User { get; set; }
    }
}

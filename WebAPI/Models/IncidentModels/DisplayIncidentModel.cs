using Data.Core.Domain;
using WebAPI.Models.UserModels;

namespace WebAPI.Models.IncidentModels
{
    public class DisplayIncidentModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public SeverityEnum Severity { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Created { get; set; }
        public DisplayUserModel Reporter { get; set; }
    }
}

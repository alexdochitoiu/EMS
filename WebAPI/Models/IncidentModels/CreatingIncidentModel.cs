using Data.Core.Domain;

namespace WebAPI.Models.IncidentModels
{
    public class CreatingIncidentModel
    {
        public string Summary { get; set; }
        public string Description { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public SeverityEnum Severity { get; set; }
        public string ReporterId { get; set; }
    }
}

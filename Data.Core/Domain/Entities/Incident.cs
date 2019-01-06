using System;
using Data.Core.Domain.Entities.Identity;
using EnsureThat;

namespace Data.Core.Domain.Entities
{
    public class Incident : BaseEntity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public double Longitude { get; private set; }
        public double Latitude { get; private set; }
        public SeverityEnum Severity { get; private set; }
        public ApplicationUser Reporter { get; private set; }

        public bool IsNear(double centerLat, double centerLng, double km)
        {
            const double ky = 40000 / 360;
            var kx = Math.Cos(Math.PI * centerLat / 180.0) * ky;
            var dx = Math.Abs(centerLng - this.Longitude) * kx;
            var dy = Math.Abs(centerLat - this.Latitude) * ky;
            return Math.Sqrt(dx * dx + dy * dy) <= km;
        }

        public static Incident Create(string title, string description, double longitude, double latitude, SeverityEnum severity, ApplicationUser reporter)
        {
            var incident = new Incident
            {
                Id = new Guid(),
                Created = DateTime.Now
            };
            incident.Update(title, description, longitude, latitude, severity, reporter);
            return incident;
        }

        public void Update(string title, string description, double longitude, double latitude, SeverityEnum severity, ApplicationUser reporter)
        {
            Validate(reporter);

            Title = title;
            Description = description;
            Longitude = longitude;
            Latitude = latitude;
            Severity = severity;
            Reporter = reporter;
            Modified = DateTime.Now;
        }

        public static void Validate(ApplicationUser reporter)
        {
            Ensure.That(reporter).IsNotNull();
        }
    }
}

using System;
using Data.Core.Domain.Entities.Identity;
using EnsureThat;

namespace Data.Core.Domain.Entities
{
    public class Announcement : BaseEntity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public SeverityEnum Severity { get; private set; }
        public ApplicationUser User { get; set; }
        public Guid UserId { get; private set; }

        public static Announcement Create(string title, string description, SeverityEnum severity, Guid userId)
        {
            var announcement = new Announcement
            {
                Id = new Guid(),
                Created = DateTime.Now
            };
            announcement.Update(title, description, severity, userId);
            return announcement;
        }

        public void Update(string title, string description, SeverityEnum severity, Guid userId)
        {
            Validate(title, description, userId);
            Title = title;
            Description = description;
            Severity = severity;
            UserId = userId;
            Modified = DateTime.Now;
        }

        public static void Validate(string title, string description, Guid userId)
        {
            Ensure.That(title)
                .IsNotNullOrEmpty()
                .HasLengthBetween(5, 50);

            Ensure.That(description)
                .IsNotNullOrEmpty()
                .HasLengthBetween(25, 1500);

            Ensure.That(userId).IsNotEmpty();
        }
    }
}

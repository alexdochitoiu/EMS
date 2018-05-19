using System;

namespace Data.Core.Domain.Entities
{
    public class Announcement : BaseEntity
    {
        // Fileds TBD

        public static Announcement Create()
        {
            var announcement = new Announcement
            {
                Id = new Guid(),
                Created = DateTime.Now
            };
            announcement.Update();
            return announcement;
        }

        public void Update()
        {
            Validate();
            Modified = DateTime.Now;
        }

        public static void Validate()
        {
            
        }
    }
}

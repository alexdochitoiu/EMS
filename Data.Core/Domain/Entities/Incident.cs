using System;

namespace Data.Core.Domain.Entities
{
    public class Incident : BaseEntity
    {
        // Fileds TBD

        public static Incident Create()
        {
            var incident = new Incident
            {
                Id = new Guid(),
                Created = DateTime.Now
            };
            incident.Update();
            return incident;
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

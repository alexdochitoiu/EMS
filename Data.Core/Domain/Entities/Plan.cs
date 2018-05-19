using System;

namespace Data.Core.Domain.Entities
{
    public class Plan : BaseEntity
    {
        // Fileds TBD

        public static Plan Create()
        {
            var plan = new Plan
            {
                Id = new Guid(),
                Created = DateTime.Now
            };
            plan.Update();
            return plan;
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

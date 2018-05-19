using System;

namespace Data.Core.Domain.Entities
{
    public class Guide : BaseEntity
    {
        // Fileds TBD

        public static Guide Create()
        {
            var guide = new Guide
            {
                Id = new Guid(),
                Created = DateTime.Now
            };
            guide.Update();
            return guide;
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

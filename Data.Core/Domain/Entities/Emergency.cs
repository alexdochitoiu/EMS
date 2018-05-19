using System;

namespace Data.Core.Domain.Entities
{
    public class Emergency : BaseEntity
    {
        // Fileds TBD

        public static Emergency Create()
        {
            var emergency = new Emergency
            {
                Id = new Guid(),
                Created = DateTime.Now
            };
            emergency.Update();
            return emergency;
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

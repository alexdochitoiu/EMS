using System;

namespace Data.Core.Domain
{
    public class Address
    {
        public Guid Id { get; set; }
        public Country Country { get; set; }
        public City City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string ZipCode { get; set; }    
    }
}

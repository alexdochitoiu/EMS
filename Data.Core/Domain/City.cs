using System;

namespace Data.Core.Domain
{
    public class City
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Abbreviation { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
    }
}

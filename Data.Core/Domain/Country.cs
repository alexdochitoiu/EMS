using System;
using System.Collections.Generic;

namespace Data.Core.Domain
{
    public class Country
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Abbreviation { get; private set; }
        public IReadOnlyCollection<City> Cities { get; private set; }
    }
}

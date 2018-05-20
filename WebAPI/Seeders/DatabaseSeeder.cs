using Data.Persistence;

namespace WebAPI.Seeders
{
    public class DatabaseSeeder
    {
        private readonly CitiesSeeder _citiesSeeder;
        private readonly CountriesSeeder _countriesSeeder;

        public DatabaseSeeder(IdentityContext identityContext)
        {
            _citiesSeeder = new CitiesSeeder(identityContext);
            _countriesSeeder = new CountriesSeeder(identityContext);
        }

        public void Seed()
        {
            _countriesSeeder.Seed();
            _citiesSeeder.Seed();
        }
    }
}

using Data.Persistence.Interfaces;

namespace WebAPI.Seeders
{
    public class DatabaseSeeder
    {
        private readonly CitiesSeeder _citiesSeeder;
        private readonly CountriesSeeder _countriesSeeder;

        public DatabaseSeeder(IDatabaseContext databaseContext)
        {
            _citiesSeeder = new CitiesSeeder(databaseContext);
            _countriesSeeder = new CountriesSeeder(databaseContext);
        }

        public void Seed()
        {
            _countriesSeeder.Seed();
            _citiesSeeder.Seed();
        }
    }
}

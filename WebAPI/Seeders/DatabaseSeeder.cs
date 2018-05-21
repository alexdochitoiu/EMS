using Data.Persistence;

namespace WebAPI.Seeders
{
    public class DatabaseSeeder
    {
        private readonly CitiesSeeder _citiesSeeder;
        private readonly CountriesSeeder _countriesSeeder;

        public DatabaseSeeder(ApplicationDbContext context)
        {
            _citiesSeeder = new CitiesSeeder(context);
            _countriesSeeder = new CountriesSeeder(context);
        }

        public void Seed()
        {
            _countriesSeeder.Seed();
            _citiesSeeder.Seed();
        }
    }
}

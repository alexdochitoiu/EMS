using System.Threading.Tasks;
using Data.Core.Interfaces;

namespace WebAPI.Seeders
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        private readonly CitiesSeeder _citiesSeeder;
        private readonly CountriesSeeder _countriesSeeder;
        private readonly AnnouncementsSeeder _announcementsSeeder;

        public DatabaseSeeder(
            CitiesSeeder cities, 
            CountriesSeeder countries,
            AnnouncementsSeeder announcements
            )
        {
            _citiesSeeder = cities;
            _countriesSeeder = countries;
            _announcementsSeeder = announcements;
        }

        public async Task<int> SeedAsync()
        {
            var totalRecords = 
                await _countriesSeeder.SeedAsync() + 
                await _citiesSeeder.SeedAsync() +
                await _announcementsSeeder.SeedAsync();

            return totalRecords;
        }
    }
}

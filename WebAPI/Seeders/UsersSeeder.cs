using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Core.Domain.Entities;
using Data.Core.Domain.Entities.Identity;
using Data.Persistence;

namespace WebAPI.Seeders
{
    public class UsersSeeder
    {
        private readonly ApplicationDbContext _context;

        public UsersSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> SeedAsync()
        {
            if (_context.Users.Any()) return -1;
            
            return await _context.SaveChangesAsync();
        }

        private static IEnumerable<ApplicationUser> GetUsers()
        {
            var users = new List<ApplicationUser>();
            var firstNames = new[]
            {
                "ion", "alex", "grigore", "andrei", "cosmin", "costel",
                "constantin", "catalin", "alin", "george", "madalin", "petru",
                "david", "denis", "florin", "silviu", "stefan", "cristi",
            };
            var lastNames = new[]
            {
                "badea", "ababei", "calinescu", "dinescu",
                "dochitoiu", "lovin", "lazar", "lazarescu", "popa",
                "popescu", "banica", "bercea", "branzescu"
            };
            foreach (var fName in firstNames)
            {
                foreach (var lName in lastNames)
                {
                    var delimiter = new[] {
                        '_', '.'
                    };
                    var randNum = new Random().Next(100);
                    var username = fName + delimiter[randNum % 2] + lName + randNum;
                    // users.Add();
                }
            }
            return users;
        }
    }
}

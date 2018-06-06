using System.Threading.Tasks;

namespace Data.Core.Interfaces
{
    public interface IDatabaseSeeder
    {
        Task<int> SeedAsync();
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebAPI.Infrastructure
{
    public static class IocContainer
    {
        public static IConfiguration Configuration { get; set; }
    }
}

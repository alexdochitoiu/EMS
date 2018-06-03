using Microsoft.Extensions.Configuration;

namespace WebAPI.Infrastructure
{
    public static class IocContainer
    {
        public static IConfiguration Configuration { get; set; }
    }
}

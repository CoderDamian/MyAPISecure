using Microsoft.EntityFrameworkCore;
using MyPersistence;

namespace MyAPI.Extensions
{
    public static class ServicesExtension
    {
        public static void AddJWT(this IServiceCollection services, IConfiguration configuration)
        { }

        public static void AddOracle(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionString:Oracle"];

            services.AddDbContext<MyDbContext>(opt => opt.UseOracle(connectionString));
        }
    }
}

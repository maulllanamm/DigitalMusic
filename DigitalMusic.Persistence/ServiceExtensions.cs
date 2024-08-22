using DigitalMusic.Application.Repositories;
using DigitalMusic.Persistence.Context;
using DigitalMusic.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalMusic.Persistence
{
    public static class ServiceExtensions
    {
        public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgreDB");
            services.AddDbContext<DataContext>(opt => opt.UseNpgsql(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
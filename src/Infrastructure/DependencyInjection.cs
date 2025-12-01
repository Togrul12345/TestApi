using Domain.Common.Interfaces;
using Domain.Common.Pagionation;
using Domain.Entities.UserEntity;
using Infrastructure.Contexts;
using Infrastructure.Persistence;
using Infrastructure.Services.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
        {
            if (env.IsDevelopment())
                services.AddDbContext<BaseDbContext,TestDbContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("Default"));
                });

            services.AddDbContext<TestDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("Default")));

            services.AddScoped(typeof(IRepository<,>), typeof(EfRepository<,>));
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddSingleton<ILocalizationService, LocalizationService>();
            services.AddScoped<PaginatedList<AppUser>>();

            return services;
        }


    }
}

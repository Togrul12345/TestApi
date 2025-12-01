using Application.Common.Behaviors;
using Domain.Common.Interfaces;
using Domain.Entities.UserEntity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;
public static class DependencyInjectionApplication
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(), typeof(IRepository<,>).Assembly));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        services.AddScoped<IPasswordHasher<AppUser>,PasswordHasher<AppUser>>();
        return services;
    }
}

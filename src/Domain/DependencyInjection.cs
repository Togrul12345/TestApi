using Domain.Security.JWT;
using Domain.Security.JWT.Configuration;
using Domain.Security.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            //services.AddScoped<ITokenHelper, JwtHelper>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            return services;
        }
    }
}

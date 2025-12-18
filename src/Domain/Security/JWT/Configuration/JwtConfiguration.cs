using Domain.Security.Encryption;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Domain.Security.JWT.Configuration;
public static class JwtConfiguration
{
    public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
         // Adding Jwt Bearer
         .AddJwtBearer(options =>
         {
             options.SaveToken = true;
             options.RequireHttpsMetadata = false;
             options.TokenValidationParameters = new TokenValidationParameters()
             {
                 ValidateIssuer = true,
                 ValidateAudience = true,
                 ValidateLifetime = true,
                 ValidateIssuerSigningKey = true,
                 ClockSkew = TimeSpan.Zero,

                 ValidAudience = configuration["TokenOptions:Audience"],
                 ValidIssuer = configuration["TokenOptions:Issuer"],
                 IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(configuration["TokenOptions:SecurityKey"]!)
             };
             options.Events = new JwtBearerEvents
             {
                 OnMessageReceived = context =>
                 {
                     var accessToken = context.Request.Query["access_token"];

                     // yalnız SignalR hub üçün
                     var path = context.HttpContext.Request.Path;
                     if (!string.IsNullOrEmpty(accessToken)
                         && path.StartsWithSegments("/ChatHub"))
                     {
                         context.Token = accessToken;
                     }

                     return Task.CompletedTask;
                 }
             };

         });
    }
}

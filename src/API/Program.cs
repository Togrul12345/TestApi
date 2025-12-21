using API.Common;
using API.Hubs;
using API.Services.ChatServices;
using API.Services.MessageServices;
using API.Services.PollServices;
using API.Services.UserServices;
using Application;
using Application.Common.Mappings;
using Domain;
using Domain.Common.Pagionation;
using Domain.Entities.UserEntity;
using Domain.Security.JWT;
using Domain.Security.JWT.Configuration;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(7097); // HTTP
    options.ListenAnyIP(7294, listenOptions => // HTTPS
    {
        listenOptions.UseHttps();
    });
});
// Controllers + JSON
builder.Services
    .AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

#region Localization
var supportedCultures = builder.Configuration.GetSection("SupportedCultures").Get<string[]>();
var cultures = new List<CultureInfo>();
supportedCultures?.ToList().ForEach(cultureName => cultures.Add(new(cultureName)));
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(supportedCultures![0]),
    SupportedCultures = cultures,
    SupportedUICultures = cultures
};
#endregion

// JWT & Domain & Application & Infrastructure
builder.Services.AddJwt(builder.Configuration);
builder.Services.AddDomain();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);
builder.Services.AddSingleton<JwtHelper>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IChatService, ChatService>();


//Database
//builder.Services.AddDbContext<TestDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString(
//        ""
//    ))
//);



// Validation & AutoMapper
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(typeof(DependencyInjectionApplication).Assembly);
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddSignalR();
// CORS
builder.Services.AddCors(opt => opt.AddDefaultPolicy(policy =>
{
    policy.AllowAnyHeader()
          .AllowAnyMethod()
          .AllowCredentials()
          .SetIsOriginAllowed(origin => true);
}));
#region Swagger
builder.Services.AddSwaggerGen(opt =>
{
    opt.CustomSchemaIds(type => type.FullName);
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
               Reference = new OpenApiReference
               {
                   Type = ReferenceType.SecurityScheme,
                   Id = "Bearer"
               }
            },
            new string[]{}
        }
    });
});
#endregion
builder.Services.AddHostedService<PollAutoCloseService>();
builder.Services.AddHostedService<MessageExpirationService>();
// Authorization
builder.Services.AddAuthorization(options =>
{
    // Default policy olmadýðý üçün null error yaranýr
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});


// Others
builder.Services.AddHealthChecks();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddHttpClient();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseHsts();
}
// CORS
app.UseCors();
// Routing, HTTPS, Static Files
app.UseRouting();


// Custom exception handler
app.UseCustomExceptionHandler();

// Localization
app.UseRequestLocalization(localizationOptions);

// Health checks
app.UseHealthChecks("/health");


app.UseHttpsRedirection();
app.UseStaticFiles();

// Swagger
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1"));

// Authorization
app.UseAuthentication();
app.UseAuthorization();


// Endpoints
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.MapHub<ChatHub>("/ChatHub");
app.Run();

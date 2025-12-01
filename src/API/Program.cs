using API.Common;
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
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

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

// CORS
builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", policy =>
{
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader();
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

// Authorization
builder.Services.AddAuthorization();

// Others
builder.Services.AddHealthChecks();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

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
app.UseCors("CorsPolicy");

// Custom exception handler
app.UseCustomExceptionHandler();

// Localization
app.UseRequestLocalization(localizationOptions);

// Health checks
app.UseHealthChecks("/health");

// Routing, HTTPS, Static Files
app.UseRouting();
app.UseHttpsRedirection();
app.UseStaticFiles();

// Swagger
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1"));

// Authorization
app.UseAuthorization();

// Endpoints
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

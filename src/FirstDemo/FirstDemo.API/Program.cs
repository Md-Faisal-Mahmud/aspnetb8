using Autofac.Extensions.DependencyInjection;
using Autofac;
using FirstDemo.Application;
using FirstDemo.Infrastructure;
using Serilog;
using Serilog.Events;
using System.Reflection;
using FirstDemo.Persistence;
using FirstDemo.Api;
using FirstDemo.Persistence.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Autofac.Core;
using FirstDemo.Infrastructure.Securities;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(builder.Configuration));

// Add services to the container.

try
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    var migrationAssembly = Assembly.GetExecutingAssembly().FullName;

    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule(new ApplicationModule());
        containerBuilder.RegisterModule(new InfrastructureModule());
        containerBuilder.RegisterModule(new PersistenceModule(connectionString,
            migrationAssembly));
        containerBuilder.RegisterModule(new ApiModule());
    });

    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddIdentity();

    builder.Services.AddAuthentication()
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
            };
        });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("CourseViewRequirementPolicy", policy =>
        {
            policy.AuthenticationSchemes.Clear();
            policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
            policy.RequireAuthenticatedUser();
            policy.Requirements.Add(new CourseViewRequirement());
        });
    });

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSites",
            builder =>
            {
                //builder.AllowAnyOrigin()
                //    .AllowAnyMethod()
                //    .AllowAnyHeader();

                builder.WithOrigins("http://localhost:4200", "https://localhost:7307", "https://localhost:9510")
                   .AllowAnyMethod()
                   .AllowAnyHeader();
            });
    });

    builder.Services.AddSingleton<IAuthorizationHandler, CourseViewRequirementHandler>();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    Log.Information("Application Starting...");

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}

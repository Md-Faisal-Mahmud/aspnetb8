using Autofac.Extensions.DependencyInjection;
using Autofac;
using FirstDemo.Web.Data;
using FirstDemo.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FirstDemo.Web;
using Serilog;
using Serilog.Events;
using FirstDemo.Persistence;
using FirstDemo.Persistence.Extensions;
using FirstDemo.Application;
using FirstDemo.Infrastructure;
using Autofac.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using FirstDemo.Infrastructure.Securities;
using Microsoft.AspNetCore.Authorization;
using FirstDemo.Domain.Utilities;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(builder.Configuration));

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
		containerBuilder.RegisterModule(new WebModule());
    });

    // Add services to the container.
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddIdentity();
    builder.Services.AddControllersWithViews();

    builder.Services.AddAuthentication()
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
        {
            options.LoginPath = new PathString("/Account/Login");
            options.AccessDeniedPath = new PathString("/Account/Login");
            options.LogoutPath = new PathString("/Account/Logout");
            options.Cookie.Name = "FirstDemoPortal.Identity";
            options.SlidingExpiration = true;
            options.ExpireTimeSpan = TimeSpan.FromHours(1);
        });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("BackOfficeStaff", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireRole("HR");
            policy.RequireRole("Accountant");
        });

        options.AddPolicy("CourseViewPolicy", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireClaim("ViewCourse", "true");
        });

        options.AddPolicy("CourseViewRequirementPolicy", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.Requirements.Add(new CourseViewRequirement());
        });
    });

    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

    builder.Services.AddSingleton<IAuthorizationHandler, CourseViewRequirementHandler>();
    
    builder.Services.Configure<Smtp>(builder.Configuration.GetSection("Smtp"));

    var app = builder.Build();


    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection()
        .UseStaticFiles()
        .UseRouting()
        .UseAuthentication()
        .UseAuthorization()
        .UseSession();

    app.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    app.MapRazorPages();

    app.Run();

    Log.Information("Application Starting...");
}
catch(Exception ex)
{
    Log.Fatal(ex, "Failed to start application.");
}
finally
{
    Log.CloseAndFlush();
}

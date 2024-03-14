using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using FirstDemo.Application;
using FirstDemo.Infrastructure;
using FirstDemo.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FirstDemo.MigrationRunner
{
    public class Program
    {
        private static string _connectionString;
        private static string _migrationAssemblyName;
        private static IConfiguration _configuration;

        public static void Main(string[] args)
        {
            DirectoryInfo root = new DirectoryInfo(Directory.GetCurrentDirectory());
            string settingsPath = Path.Combine(root.Parent.Parent.Parent.FullName, "appsettings.json");

            _configuration = new ConfigurationBuilder().AddJsonFile(settingsPath, false)
                .AddEnvironmentVariables()
                .Build();

            _connectionString = _configuration.GetConnectionString("DefaultConnection");

            _migrationAssemblyName = typeof(Program).Assembly.FullName;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(_configuration)
                .CreateLogger();

            try
            {
                CreateHostBuilder(args).Build().Run();

                Log.Information("Application Starting up");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((services) => services.AddDbContextFactory<ApplicationDbContext>(options =>
                    options.UseSqlServer(_connectionString, m => m.MigrationsAssembly(_migrationAssemblyName))))
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .UseSerilog()
                .ConfigureContainer<ContainerBuilder>(builder =>
                {

                    builder.RegisterModule(new PersistenceModule
                             (_connectionString, _migrationAssemblyName));
                    builder.RegisterModule(new ApplicationModule());
                    builder.RegisterModule(new InfrastructureModule());
                });
    }
}

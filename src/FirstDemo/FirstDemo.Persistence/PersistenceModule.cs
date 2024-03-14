using Autofac;
using FirstDemo.Application;
using FirstDemo.Application.Features.Training.Repositories;
using FirstDemo.Persistence.Features.Training.Repositories;
using FirstDemo.Persistence;
using Microsoft.EntityFrameworkCore;
using FirstDemo.Domain.Utilities;

namespace FirstDemo.Persistence
{
    public class PersistenceModule : Module
    {
		private readonly string _connectionString;
		private readonly string _migrationAssemblyName;

		public PersistenceModule(string connectionString, string migrationAssemblyName)
		{
			_connectionString = connectionString;
			_migrationAssemblyName = migrationAssemblyName;
		}

        protected override void Load(ContainerBuilder builder)
        {
			builder.RegisterType<CourseRepository>().As<ICourseRepository>()
				.InstancePerLifetimeScope();

			builder.RegisterType<ApplicationDbContext>().AsSelf()
				.WithParameter("connectionString", _connectionString)
				.WithParameter("migrationAssembly", _migrationAssemblyName)
				.InstancePerLifetimeScope();

			builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
				.WithParameter("connectionString", _connectionString)
				.WithParameter("migrationAssembly", _migrationAssemblyName)
				.InstancePerLifetimeScope();

			builder.Register(x => new AdoNetUtility(
				x.Resolve<ApplicationDbContext>().Database.GetDbConnection(), 300))
				.As<IAdoNetUtility>().InstancePerLifetimeScope();

			builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
				.InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}

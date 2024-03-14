using Autofac;
using FirstDemo.Application;
using FirstDemo.Application.Features.Training.Repositories;
using FirstDemo.Application.Features.Training.Services;
using FirstDemo.Domain.Services;
using FirstDemo.Infrastructure.Features.Services;
using FirstDemo.Infrastructure.Securities;
using FirstDemo.Infrastructure.Services;

namespace FirstDemo.Infrastructure
{
    public class InfrastructureModule : Module
    {
		public InfrastructureModule()
        { 
		}

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CourseService>().As<ICourseService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EnrollmentService>().As<IEnrollmentService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TokenService>().As<ITokenService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<HtmlEmailService>().As<IEmailService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EmailMessageService>().As<IEmailMessageService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}

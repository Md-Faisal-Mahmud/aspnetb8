using Autofac;
using FirstDemo.Application;
using FirstDemo.Application.Features.Training.Repositories;
using FirstDemo.Web.Areas.Admin.Models;
using FirstDemo.Web.Models;

namespace FirstDemo.Web
{
    public class WebModule : Module
    {
		public WebModule()
        { }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CourseListModel>().AsSelf();

            builder.RegisterType<CourseCreateModel>().AsSelf();

            builder.RegisterType<CourseUpdateModel>().AsSelf();

            builder.RegisterType<RegisterModel>().AsSelf();

            builder.RegisterType<LoginModel>().AsSelf();

            builder.RegisterType<RoleListModel>().AsSelf();

            builder.RegisterType<RoleCreateModel>().AsSelf();

            builder.RegisterType<RoleAssignModel>().AsSelf();

            builder.RegisterType<EnrollmentListModel>().AsSelf();

            base.Load(builder);
        }
    }
}

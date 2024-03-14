using Autofac;
using FirstDemo.Application;
using FirstDemo.Application.Features.Training.Repositories;
using FirstDemo.Application.Features.Training.Services;

namespace FirstDemo.Application
{
    public class ApplicationModule : Module
    {
		public ApplicationModule()
        { 
		}

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
        }
    }
}

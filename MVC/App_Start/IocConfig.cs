using Application.HumanResources.Commands.CreateHumanResource;
using Autofac;
using Autofac.Integration.Mvc;
using Infrastructure.Repositories;
using MediatR;
using System.Web.Mvc;

namespace MVC
{
    public static class IocConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterAssemblyTypes(typeof(HumanResourceRepository).Assembly).AsImplementedInterfaces().SingleInstance();
            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly).AsImplementedInterfaces(); 
            builder.RegisterAssemblyTypes(typeof(CreateHumanResourceCommand).Assembly).AsImplementedInterfaces();

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
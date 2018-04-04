using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.FrameWork
{
    public class AutofacBuilder
    {
        public static IContainer Builder(IServiceCollection services)
        {
            // Create the container builder.
            var containbuilder = new ContainerBuilder();

            // Register dependencies, populate the services from
            // the collection, and build the container. If you want
            // to dispose of the container at the end of the app,
            // be sure to keep a reference to it as a property or field.
            //
            // Note that Populate is basically a foreach to add things
            // into Autofac that are in the collection. If you register
            // things in Autofac BEFORE Populate then the stuff in the
            // ServiceCollection can override those things; if you register
            // AFTER Populate those registrations can override things
            // in the ServiceCollection. Mix and match as needed.
            containbuilder.Populate(services);
            SetupResolveRules(containbuilder);

            return containbuilder.Build();
        }

        //注入
        private static void SetupResolveRules(ContainerBuilder builder)
        {
            //跨程序集注册
            var descriptorsAccounts = Assembly.Load("EmergencyAccount");
            builder.RegisterAssemblyTypes(descriptorsAccounts)
                .Where(t => t.Name.EndsWith("Service") && !t.IsAbstract)
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            var descriptorsPaperNews = Assembly.Load("PaperNewsService");
            builder.RegisterAssemblyTypes(descriptorsPaperNews)
                .Where(t => t.Name.EndsWith("Service") && !t.IsAbstract)
                .AsImplementedInterfaces().InstancePerLifetimeScope();
        }


        /// <summary>
        /// 发布订阅注入
        /// </summary>
        /// <param name="builder"></param>
        private static void SetupMediatRRules(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces().PropertiesAutowired();

            var mediatrOpenTypes = new[]
          {
                typeof(IRequestHandler<,>),
                typeof(IRequestHandler<>),
                typeof(INotificationHandler<>),
            };

            var descriptorsPaperNews = Assembly.Load("PaperNewsService");
            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder
                    .RegisterAssemblyTypes(descriptorsPaperNews).Where(t => t.Name.EndsWith("Commands") && !t.IsAbstract)
                    .AsClosedTypesOf(mediatrOpenType)
                    .AsImplementedInterfaces();
            }
            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));

            builder.Register<SingleInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.Register<MultiInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
            });
        }
    }
}

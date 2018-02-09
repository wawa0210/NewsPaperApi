using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EmergencyAccount.Application;
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

            //containbuilder.RegisterType<AccountService>().As<IAccountService>().InstancePerRequest();

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

    }
}

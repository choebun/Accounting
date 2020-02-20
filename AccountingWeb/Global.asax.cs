using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AccountingWeb.Models.Services;
using Autofac;
using Autofac.Integration.Mvc;

namespace AccountingWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutofacConfig.Register();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }

    public class AutofacConfig
    {
        private static IContainer _container;

        public static void Register()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<BudgetService>().As<IBudgetService>();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            _container = builder.Build();

            var resolver = new AutofacDependencyResolver(_container);
            DependencyResolver.SetResolver(resolver);
        }
    }
}
using System;
using System.Web.Mvc;
using System.Web.Routing;

using BetterCms.Core.Mvc.Commands;

using Common.Logging;
using DevBridge.Templates.WebProject.Data;
using DevBridge.Templates.WebProject.Data.DataContext;
using DevBridge.Templates.WebProject.DataContracts;
using DevBridge.Templates.WebProject.ServiceContracts;
using DevBridge.Templates.WebProject.Services;
using DevBridge.Templates.WebProject.Tools.Commands;

using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Unity.Mvc3;

namespace DevBridge.Templates.WebProject.Web
{
    public class WebApplication : System.Web.HttpApplication
    {
        private static readonly ILog log = LogManager.GetCurrentClassLogger();

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            try
            {
                InitializeDependencyInjectionContainer();

                AreaRegistration.RegisterAllAreas();
                RegisterGlobalFilters(GlobalFilters.Filters);
                RegisterRoutes(RouteTable.Routes);
                
                log.Info("Site started...");
            }
            catch (Exception ex)
            {
                log.Fatal("Failed to start site.", ex);
                throw;
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            log.Fatal("Unhandled exception occurred.", ex);
        }

        protected void Application_End(object sender, EventArgs e)
        {
            log.Info("Site stopped...");
        }

        private static void InitializeDependencyInjectionContainer()
        {
            var container =
                new UnityContainer().RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager())
                                    .RegisterType<ISessionFactoryProvider, SessionFactoryProvider>(new ContainerControlledLifetimeManager())
                                    .RegisterType<IUnitOfWorkFactory, UnitOfWorkFactory>(new ContainerControlledLifetimeManager())
                                    .RegisterType<IAgreementManagementService, AgreementManagementService>(new HierarchicalLifetimeManager())
                                    .RegisterType<IRegistrationService, RegistrationService>(new HierarchicalLifetimeManager())
                                    .RegisterType<IDataListingService, DataListingService>(new HierarchicalLifetimeManager())
                                    .RegisterType<ICachingService, CachingService>(new ContainerControlledLifetimeManager())
                                    .RegisterType<IConfigurationLoaderService, ConfigurationLoaderService>(new ContainerControlledLifetimeManager())
                                    .RegisterType<ICommandResolver, DefaultCommandResolver>(new HierarchicalLifetimeManager());

            var serviceLocator = new UnityServiceLocator(container);

            ServiceLocator.SetLocatorProvider(() => serviceLocator);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
using System;
using System.Web.Mvc;
using System.Web.Routing;
using Common.Logging;
using DevBridge.Templates.WebProject.Data;
using DevBridge.Templates.WebProject.Data.DataContext;
using DevBridge.Templates.WebProject.DataContracts;
using DevBridge.Templates.WebProject.ServiceContracts;
using DevBridge.Templates.WebProject.Services;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Unity.Mvc3;

namespace DevBridge.Templates.WebProject.Web
{
    public class WebApplication : System.Web.HttpApplication
    {
        private static readonly ILog log = LogManager.GetCurrentClassLogger();
        private const string ApplicationName = "DevBridge.Templates.WebProject";

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
                
                log.Info(string.Format("{0} site started...", ApplicationName));
            }
            catch (Exception ex)
            {
                log.Fatal(string.Format("Failed to start {0} site.", ApplicationName), ex);
                throw;
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            log.Fatal(string.Format("Unhandled exception occurred in {0}.", ApplicationName), ex);
        }

        protected void Application_End(object sender, EventArgs e)
        {
            log.Info(string.Format("{0} site stopped...", ApplicationName));
        }

        private static void InitializeDependencyInjectionContainer()
        {
            var container = new UnityContainer()
                .RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager())
                .RegisterType<ISessionFactoryProvider, SessionFactoryProvider>(new ContainerControlledLifetimeManager())
                .RegisterType<IUnitOfWorkFactory, UnitOfWorkFactory>(new ContainerControlledLifetimeManager())

                .RegisterType<IAgreementManagementService, AgreementManagementService>(new HierarchicalLifetimeManager())
                .RegisterType<IRegistrationService, RegistrationService>(new HierarchicalLifetimeManager())
                .RegisterType<IDataListingService, DataListingService>(new HierarchicalLifetimeManager())

                .RegisterType<IAgreementRepository, AgreementRepository>(new HierarchicalLifetimeManager())
                .RegisterType<ICustomerRepository, CustomerRepository>(new HierarchicalLifetimeManager())

                .RegisterType<ICachingService, CachingService>(new ContainerControlledLifetimeManager())
                .RegisterType<IConfigurationLoaderService, ConfigurationLoaderService>(new ContainerControlledLifetimeManager());

            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(container));
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
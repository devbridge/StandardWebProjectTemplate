using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

using Autofac;
using Autofac.Integration.Mvc;

using BetterCms.Core.Mvc.Commands;

using Common.Logging;
using DevBridge.Templates.WebProject.Data.DataContext;
using DevBridge.Templates.WebProject.DataContracts;
using DevBridge.Templates.WebProject.ServiceContracts;
using DevBridge.Templates.WebProject.Services;
using DevBridge.Templates.WebProject.Tools.Commands;
using DevBridge.Templates.WebProject.Web.Logic;
using DevBridge.Templates.WebProject.Web.Logic.Commands.Agreement.GetAgreements;

using Microsoft.Practices.ServiceLocation;


namespace DevBridge.Templates.WebProject.Web
{
    public class WebApplication : System.Web.HttpApplication
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

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
                var container = new ContainerFactory().CreateContainer(typeof(WebApplication).Assembly);
                DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

                AreaRegistration.RegisterAllAreas();
                RegisterGlobalFilters(GlobalFilters.Filters);
                RegisterRoutes(RouteTable.Routes);
                
                Log.Info("Site started...");
            }
            catch (Exception ex)
            {
                Log.Fatal("Failed to start site.", ex);
                throw;
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            Log.Fatal("Unhandled exception occurred.", ex);
        }

        protected void Application_End(object sender, EventArgs e)
        {
            Log.Info("Site stopped...");
        }

        
    }
}
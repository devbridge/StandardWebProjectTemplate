using System;
using System.Web.Mvc;
using System.Web.Routing;
using DevBridge.Templates.WebProject.AutocompleteDemo.Infrastructure;
using DevBridge.Templates.WebProject.AutocompleteDemo.Infrastructure.ModelBinders;

namespace DevBridge.Templates.WebProject.AutocompleteDemo
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
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

		public static void RegisterModelBinders(ModelBinderDictionary binders)
		{
			ModelBinders.Binders[typeof(Autocomplete)] = new AutocompleteModelBinder();
			ModelBinders.Binders[typeof(Type)] = new TypeModelBinder();
		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			DependencyRegistrar.Register();
			ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory()); 

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);

			ValueProviderFactories.Factories.Add(new JsonValueProviderFactory());
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
//using FiveThreeOne.Service;
//using FiveThreeOne.Service.Impl;
using Ninject;

namespace FiveThreeOne.Web {
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication {
		public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				 "Default", // Route name
				 "{controller}/{action}/{id}", // URL with parameters
				 new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);

		}

		public void SetupDependencyInjection(string connectionString) {
			IKernel kernel = new StandardKernel();

			//kernel.Bind<IIdentification>().ToMethod(x => new PlainIdentifier(connectionString));
			//kernel.Bind<IExercise>().ToMethod(x => new Exercise(connectionString));
			
			DependencyResolver.SetResolver( t => kernel.Get(t), t => kernel.GetAll(t) );
		}

		protected void Application_Start() {
			AreaRegistration.RegisterAllAreas();

			SetupDependencyInjection(System.Configuration.ConfigurationManager.AppSettings["SQLSERVER_CONNECTION_STRING"]);

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
		}
	}
}
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Serilog;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;

namespace IA.Weather.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var log = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .CreateLogger();

            var container = new Container();

            container.Register<IApiClient, ApiClient>(Lifestyle.Transient);
            container.Register(() => Log.Logger, Lifestyle.Singleton);

            container.Verify();

            // 4. Store the container for use by the application
            DependencyResolver.SetResolver(
                new SimpleInjectorDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}

using System.Web.Http;

namespace IA.Weather.API.Helpers
{
    //Required to keep our controllers unit testable
    //We use this to provide urls for routes to build a HATEOAS style api
    public interface IRouteProvider
    {
        string Route(ApiController controller, string routeName, object routeValues);
    }

    public class RouteProvider : IRouteProvider
    {
        public string Route(ApiController controller, string routeName, object routeValues)
        {
            return controller.Url.Link(routeName, routeValues);
        }
    }
}

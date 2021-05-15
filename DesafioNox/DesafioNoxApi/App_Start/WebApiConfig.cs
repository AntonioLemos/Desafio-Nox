using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace DesafioNoxApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            var jsonFormater = config.Formatters.JsonFormatter;
            jsonFormater.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
            jsonFormater.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            jsonFormater.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

    }
}

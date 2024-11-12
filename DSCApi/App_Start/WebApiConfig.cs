
using DSCApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;


namespace DSCApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web
            //config.EnableCors(new EnableCorsAttribute("http://app.facefocuscontrol.com", "*", "GET,POST,PUT,DELETE"));
            config.EnableCors(new EnableCorsAttribute("http://localhost:60710", "*", "GET,POST,PUT,DELETE"));
            // Rutas de API web
            config.MapHttpAttributeRoutes();

            //token
            config.MessageHandlers.Add(new TokenValidationHandler());


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
        }
    }
}

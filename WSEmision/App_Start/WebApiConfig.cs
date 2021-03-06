﻿using System.Web.Http;

namespace WSEmision
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "CoaseguroApi",
                routeTemplate: "api/coaseguro/{action}/{idPv}",
                defaults: new { controller = "CoaseguroWS" }
            );

            config.Routes.MapHttpRoute(
                name: "CaratulaDanosApi",
                routeTemplate: "api/danos/{action}/{idPv}",
                defaults: new { controller = "CaratulaDanosWS" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

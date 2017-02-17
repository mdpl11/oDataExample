namespace ProductServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using Models;
    using System.Web.OData.Builder;
    using System.Web.OData.Extensions;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            ODataModelBuilder builder = new ODataConventionModelBuilder();

            builder.EntitySet<Product>("Products");
            builder.EntityType<Product>().Filter("Name", "Price", "Category");
            builder.EntityType<Product>().OrderBy(System.Web.OData.Query.QueryOptionSetting.Allowed, "Name", "Price");
            builder.EntityType<Product>().Count();
            builder.EntityType<Product>().Expand();
            builder.EntityType<Product>().Select();

            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: "odata",
                model: builder.GetEdmModel());
        }
    }
}
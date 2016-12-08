using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Ninject;
using Ninject.Extensions.Interception.Infrastructure.Language;
using OneApp.Common.WebServices.Filters;
using OneApp.Common.WebServices.Handlers;
using OneApp.Common.WebServices.Interceptors;
using OneApp.Common.WebServices.Selectors;

namespace OneApp.Common.WebServices
{
    public static class WebApiConfig
    {
        public static readonly HttpConfiguration Configuration = new HttpConfiguration();

        public static void Register(HttpConfiguration config)
        {
             
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            config.Routes.MapHttpRoute(
                name: "RedirectToIndex",
                routeTemplate: "",
                 defaults: new { controller = "Error", action = "RedirectToIndex" }
            );
            config.Routes.MapHttpRoute(
             name: "Error404",
             routeTemplate: "{*url}",
              defaults: new { controller = "Error", action = "Handle404" }
         );


            //handlers
            config.MessageHandlers.Add(new OneAppWebApiHandler());
             
            //services : handle not found error
            config.Services.Replace(typeof(IHttpControllerSelector), new HttpNotFoundControllerSelector(config));
            config.Services.Replace(typeof(IHttpActionSelector), new HttpNotFoundControllerActionSelector());

            //Filters
            //action filters gets executed before and after action
            config.Filters.Add(new ParametersValidatorActionFilter());

            //exception filter is reponsible for handling errors while executing actions
            config.Filters.Add(new OneAppExceptionFilter());

            //formatters
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);


        }

       
    }
}

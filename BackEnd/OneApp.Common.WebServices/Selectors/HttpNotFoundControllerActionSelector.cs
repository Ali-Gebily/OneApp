using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using OneApp.Common.WebServices.Controllers;

namespace OneApp.Common.WebServices.Selectors
{
    public class HttpNotFoundControllerActionSelector : ApiControllerActionSelector
    {
        public HttpNotFoundControllerActionSelector()
        {
        }
        public override HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
        {
            HttpActionDescriptor decriptor = null;
            try
            {
                decriptor = base.SelectAction(controllerContext);
            }

            catch (HttpResponseException ex)
            when (ex.Response.StatusCode == HttpStatusCode.NotFound ||
            ex.Response.StatusCode == HttpStatusCode.MethodNotAllowed)
            {

                controllerContext.ControllerDescriptor =
                                 new HttpControllerDescriptor(controllerContext.Configuration, "Error", typeof(ErrorController));


                var routeData = controllerContext.RouteData;
                routeData.Values["action"] = ex.Response.StatusCode == HttpStatusCode.NotFound ? "Handle404" : "Handle405";
                controllerContext.Controller = new ErrorController();


                routeData.Values["MS_SubRoutes"] = null;

                decriptor = base.SelectAction(controllerContext);

            }

            return decriptor;

        }
    }


}
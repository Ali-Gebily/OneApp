using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using OneApp.Common.Core.Managers;
using OneApp.Common.Core.Utilities;
using OneApp.Common.WebServices.Exceptions;
using OneApp.Common.WebServices.Models;

namespace OneApp.Common.WebServices.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ParametersValidatorActionFilter : ActionFilterAttribute
    {
        // Cache used to store the required parameters for each request based on the
        // request's http method and local path.
        private readonly ConcurrentDictionary<Tuple<HttpMethod, string>, List<string>> _Cache =
            new ConcurrentDictionary<Tuple<HttpMethod, string>, List<string>>();

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            ValidateRequiredParameters(actionContext);
            ValidateModel(actionContext);
            base.OnActionExecuting(actionContext);
        }


        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception == null)
            {
                var data = $"service= {actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName}, action={actionExecutedContext.ActionContext.ActionDescriptor.ActionName}";
                if (actionExecutedContext.Response == null)
                {
                    throw new BusinessHttpResponseException($"You can't return null from an action at {data}");
                }
                else
                {
                    if ( !(actionExecutedContext.Response.Content is OneAppStringContent))
                    {
                        throw new BusinessHttpResponseException($"Coding Error: All actions of ApiControllers should return BaseHttpActionResult service= {actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName}, action={actionExecutedContext.ActionContext.ActionDescriptor.ActionName}");
                    }
                }
            }
            base.OnActionExecuted(actionExecutedContext);

        }

        private void ValidateModel(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                throw new BusinessHttpResponseException(HttpStatusCode.BadRequest, new ErrorResponse(actionContext.ModelState));
            }
        }
        private void ValidateRequiredParameters(HttpActionContext actionContext)
        {
            // Get the request's required parameters.
            List<string> requiredParameters = this.GetRequiredParameters(actionContext);

            // If the list of required parameters is null or containst no parameters 
            // then there is nothing to validate.  
            // Return true.
            if (requiredParameters == null || requiredParameters.Count == 0)
            {
                return;
            }

            // If the required parameters are valid then continue with the request.
            // Otherwise, return status code 400.


            // Attempt to find at least one required parameter that is null.
            var EmptyOrNullParameters =
                actionContext
                .ActionArguments
                .Where(a => requiredParameters.Contains(a.Key)
                && (a.Value == null || (a.Value is string && (a.Value as string) == string.Empty))).ToList();

            // If a null required paramter was found then return false.  
            // Otherwise, return true.
            if (EmptyOrNullParameters.Count > 0)
            {
                var listOfErrors = new List<string>();
                foreach (var item in EmptyOrNullParameters)
                {
                    listOfErrors.Add($"parameter {item.Key} is required");
                }
                var errorResponse = new ErrorResponse(listOfErrors);

                throw new BusinessHttpResponseException(HttpStatusCode.BadRequest, errorResponse);
            }
        }

        private List<string> GetRequiredParameters(HttpActionContext actionContext)
        {
            // Instantiate a list of strings to store the required parameters.
            List<string> result = null;

            // Instantiate a tuple using the request's http method and the local path.
            // This will be used to add/lookup the required parameters in the cache.
            Tuple<HttpMethod, string> request =
                new Tuple<HttpMethod, string>(
                    actionContext.Request.Method,
                    actionContext.Request.RequestUri.LocalPath);

            // Attempt to find the required parameters in the cache.
            if (!this._Cache.TryGetValue(request, out result))
            {
                // If the required parameters were not found in the cache then get all
                // parameters decorated with the 'RequiredAttribute' from the action context.
                result =
                    actionContext
                    .ActionDescriptor
                    .GetParameters()
                    .Where(p => p.GetCustomAttributes<RequiredAttribute>().Any())
                    .Select(p => p.ParameterName)
                    .ToList();

                // Add the required parameters to the cache.
                this._Cache.TryAdd(request, result);
            }

            // Return the required parameters.
            return result;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using OneApp.Common.Core.Managers;
using OneApp.Common.WebServices.Managers;

namespace OneApp.Common.WebServices.Filters
{
    /// <summary>
    /// this filter will handle exceptions in OnActionExecuting,  OnActionExecuted in action filters as well as exceptions in the action itself
    /// Note: Something to have in mind is that the ExceptionFilterAttribute will be ignored if the ApiController action method throws a HttpResponseException. 
    /// </summary>
    public class OneAppExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            ExceptionManager.Instance.Process(context.Exception);
        }
    }
}
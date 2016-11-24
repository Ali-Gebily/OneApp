using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using OneApp.Common.Core.Exceptions;
using OneApp.Common.Core.Managers;
using OneApp.Common.Core.Managers.Logs;
using OneApp.Common.Core.Managers.Settings;
using OneApp.Common.Core.Utilities;
using OneApp.Common.WebServices.Exceptions;
using OneApp.Common.WebServices.Models;

namespace OneApp.Common.WebServices.Managers
{
    public class ExceptionManager
    {
        readonly static ExceptionManager _instance = new ExceptionManager();

        public static ExceptionManager Instance
        {
            get { return _instance; }
        }

        public void Process(Exception ex1)
        {
            var ex = ex1.GetMostInnerException();

            if (ex is BusinessException)
            {
                if (ex is BusinessHttpResponseException)
                {

                    throw new HttpResponseException(new HttpResponseMessage((ex as BusinessHttpResponseException).StatusCode)
                    {
                        Content = new OneAppStringContent((ex as BusinessHttpResponseException).ErrorResponse),
                        ReasonPhrase = "Business Error"
                    });
                }
                else {
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new OneAppStringContent(new ErrorResponse((ex as BusinessException).Message)),
                        ReasonPhrase = "Business Error"
                    });
                }

            }
            //Log Critical errors
            LogsManager.Error(ex);
            string message = OneAppConfigurationKeys.ShowException ? ex.Message : "An error occurred, please try again or contact the administrator.";
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new OneAppStringContent(new ErrorResponse(message)),
                ReasonPhrase = "Unexpect Error"
            });
        }
    }
}
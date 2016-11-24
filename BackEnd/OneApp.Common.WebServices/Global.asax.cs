using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;
using OneApp.Common.Core.Managers.Logs;
using OneApp.Common.WebServices.Managers;

namespace OneApp.Common.WebServices
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

            // Get the exception object.
            Exception exc = Server.GetLastError();

            // Log the exception and notify system operators
            try
            {
                ExceptionManager.Instance.Process(exc);
            }
            catch (HttpResponseException ex)
            {
                Task.Run(async () =>
                {
                    Response.Write(await ex.Response.Content.ReadAsStringAsync());
                    Response.StatusCode = (int)ex.Response.StatusCode;
                }).Wait();

            }
            catch (Exception ex)
            {
                Response.Write(new OneApp.Common.WebServices.Models.ErrorResponse(exc.Message));
            }

            // Clear the error from the server
            Server.ClearError();


        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
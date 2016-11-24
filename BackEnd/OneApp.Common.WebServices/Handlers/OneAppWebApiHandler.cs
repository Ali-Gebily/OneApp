using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using OneApp.Common.Core.Managers;
using OneApp.Common.Core.Managers.Logs;
using OneApp.Common.WebServices.Managers;

namespace OneApp.Common.WebServices.Handlers
{
    public class OneAppWebApiHandler : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            try
            {
                var time = DateTime.Now.Ticks;
                LogsManager.Info("Begining Request");
                LogsManager.Info("Url: " + request.RequestUri.AbsoluteUri);
                LogsManager.Info("User: " + Thread.CurrentPrincipal?.Identity?.Name);

                var response = await base.SendAsync(request, cancellationToken);
                time = (DateTime.Now.Ticks - time)/(long)1E4;//tick=1E7, time in ms= tick/1e(7-3)
                response.Headers.Add("execution_time", time.ToString());

                return response;

            }
            catch (Exception ex)
            {
                ExceptionManager.Instance.Process(ex);
                return null;//this line won't be reached becaue the process method will throw exception
            }

        }

    }
}
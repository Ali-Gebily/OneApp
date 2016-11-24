using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace OneApp.Common.WebServices.Models
{
    public abstract class BaseHttpActionResult: IHttpActionResult
    {
        private readonly BaseResponse _response;
        private readonly HttpStatusCode _statusCode;

        public BaseHttpActionResult(HttpStatusCode statusCode, BaseResponse responose)
        {
            _statusCode = statusCode;
            _response = responose;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = new HttpResponseMessage(_statusCode)
            {
                Content = new OneAppStringContent(_response)
            };
            return Task.FromResult(response);
        }
    }
}
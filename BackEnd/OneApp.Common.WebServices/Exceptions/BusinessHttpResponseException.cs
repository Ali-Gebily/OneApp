using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using OneApp.Common.Core.Exceptions;
using OneApp.Common.WebServices.Models;

namespace OneApp.Common.WebServices.Exceptions
{
    public class BusinessHttpResponseException : BusinessException
    {
        public readonly HttpStatusCode StatusCode;
        private ErrorResponse _errorResponse = null;
        public ErrorResponse ErrorResponse { get { return _errorResponse; } }
        public BusinessHttpResponseException(string message) :
          base()
        {
            StatusCode = HttpStatusCode.InternalServerError;
            _errorResponse = new ErrorResponse(message);

        }
        public BusinessHttpResponseException(HttpStatusCode statusCode, string message) :
            base()
        {
            StatusCode = statusCode;
            _errorResponse = new ErrorResponse(message);

        }
        public BusinessHttpResponseException(HttpStatusCode statusCode, ErrorResponse errorResponse) :
         base()
        {
            if (errorResponse == null)
            {
                throw new ArgumentNullException(nameof(errorResponse));
            }
            StatusCode = statusCode;
            _errorResponse = errorResponse;

        }

        public override string Message
        {
            get
            {
                return JsonConvert.SerializeObject(_errorResponse);
            }
        }


    }
}
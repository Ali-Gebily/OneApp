using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace OneApp.Common.WebServices.Models
{
    public class ErrorHttpActionResult: BaseHttpActionResult
    {
        public ErrorHttpActionResult(HttpStatusCode statusCode, ErrorResponse errorResponse) :
            base(statusCode, errorResponse)
        {
        }
        public static ErrorHttpActionResult GenerateBadRequest(string error)
        {
            return new ErrorHttpActionResult(HttpStatusCode.BadRequest, new ErrorResponse(error));

        }

    }
}
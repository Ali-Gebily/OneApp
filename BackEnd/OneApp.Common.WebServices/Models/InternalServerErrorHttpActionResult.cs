using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web; 

namespace OneApp.Common.WebServices.Models
{
    public class InternalServerErrorHttpActionResult : ErrorHttpActionResult
    {
        public InternalServerErrorHttpActionResult(string message) :
            base(HttpStatusCode.InternalServerError,
                new ErrorResponse(message)
                )
        {
        }
        public InternalServerErrorHttpActionResult() : this("Internal Server Error")
        { }

    }
}
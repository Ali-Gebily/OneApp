using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using OneApp.Common.WebServices.Exceptions;
using OneApp.Common.WebServices.Models;

namespace OneApp.Common.WebServices.Controllers
{
    public class ErrorController : BaseApiController
    {
        
        public ErrorController()
        {
            
        }
        [HttpGet, HttpPost, HttpPut, HttpDelete, HttpHead, HttpOptions, AcceptVerbs("PATCH")]
        public ErrorHttpActionResult Handle404()
        {
            return new ErrorHttpActionResult(HttpStatusCode.NotFound,
              new ErrorResponse("The requested resource is not found"));
        }

        [HttpGet, HttpPost, HttpPut, HttpDelete, HttpHead, HttpOptions, AcceptVerbs("PATCH")]
        public ErrorHttpActionResult Handle405()
        {

            return new ErrorHttpActionResult(HttpStatusCode.MethodNotAllowed,
               new ErrorResponse("This http verb is not allowed for this api"));

        }

        [HttpGet]
        public ErrorHttpActionResult RedirectToIndex()
        {
            HttpContext.Current.Response.Headers.Add("Location", "frontend/index.html");
            return new ErrorHttpActionResult(HttpStatusCode.Redirect,
               new ErrorResponse(""));
      
        }




        // Test Methods

        [HttpGet, HttpPost, HttpPut, HttpDelete, HttpHead, HttpOptions, AcceptVerbs("PATCH")]
        public void TestVoid()
        {
            return;
        }

        [HttpGet, HttpPost, HttpPut, HttpDelete, HttpHead, HttpOptions, AcceptVerbs("PATCH")]
        public string TestNull()
        {
            return null;
        }

        [HttpGet, HttpPost, HttpPut, HttpDelete, HttpHead, HttpOptions, AcceptVerbs("PATCH")]
        public string TestString()
        {
            return "string1";
        }


        [HttpGet, HttpPost, HttpPut, HttpDelete, HttpHead, HttpOptions, AcceptVerbs("PATCH")]
        public string TestThrowException()
        {
            throw new Exception("Exception");
        }

        [HttpGet, HttpPost, HttpPut, HttpDelete, HttpHead, HttpOptions, AcceptVerbs("PATCH")]
        public string TestThrowBusinessHttpResponseException()
        {
            throw new BusinessHttpResponseException("BusinessHttpResponseException");
        }
    }
}
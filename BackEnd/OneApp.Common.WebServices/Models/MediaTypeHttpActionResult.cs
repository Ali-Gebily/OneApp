using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace OneApp.Common.WebServices.Models
{
    public class MediaTypeHttpActionResult : BaseHttpActionResult
    {

        public MediaTypeHttpActionResult(MediaTypeResponse mediaResponse) :
            base(HttpStatusCode.OK,mediaResponse)
        {
        }
        public MediaTypeHttpActionResult(HttpStatusCode statusCode,MediaTypeResponse mediaResponse) :
           base(statusCode, mediaResponse)
        {
        }
    }
}
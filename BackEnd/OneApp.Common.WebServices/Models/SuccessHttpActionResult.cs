using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace OneApp.Common.WebServices.Models
{
    public class SuccessHttpActionResult : BaseHttpActionResult
    {

        public SuccessHttpActionResult(SuccessResponse successResponse) :
            base(HttpStatusCode.OK, successResponse)
        {
        }
        public SuccessHttpActionResult(object result) :
            base(HttpStatusCode.OK, new SuccessResponse(result))
        {
        }
        public SuccessHttpActionResult() :
          base(HttpStatusCode.OK, new SuccessResponse())
        {
        }

    }
}
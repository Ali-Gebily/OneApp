using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace OneApp.Common.WebServices.Models
{
    public class SuccessResponse : BaseResponse
    {

        [JsonProperty("result")]
        public object Result { get; set; }

        public SuccessResponse()
        {

        }
        public SuccessResponse(object result)
        {
            this.Result = result;
        }
    }
}
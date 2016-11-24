using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace OneApp.Common.WebServices.Models
{
    public class ErrorDetail
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        public ErrorDetail(string message)
        {
            Code = 0;
            Message = message;
        }

        public ErrorDetail(int code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
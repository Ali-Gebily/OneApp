using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneApp.Common.WebServices.Models
{
    public class MediaTypeResponse : BaseResponse
    {

        public byte[] Data { get; set; }
        public string ContentType { get; set; }

        public MediaTypeResponse(byte[] data, string contentType)
        {
            this.Data = data;
            ContentType = contentType;
        }
    }
}
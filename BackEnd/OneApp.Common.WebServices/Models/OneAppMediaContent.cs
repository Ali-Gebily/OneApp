using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace OneApp.Common.WebServices.Models
{
    public class OneAppMediaContent: ByteArrayContent, IOneAppContent
    {
        public OneAppMediaContent(MediaTypeResponse response): base(response.Data)
        {
            this.Headers.ContentType = new MediaTypeHeaderValue(response.ContentType);

        }
        private MediaTypeResponse _response;

       
    }
}
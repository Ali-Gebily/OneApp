using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace OneApp.Common.WebServices.Models
{
    public class OneAppStringContent : StringContent, IOneAppContent
    {
        private BaseResponse _response;

        public OneAppStringContent(BaseResponse response): base(response.ToString())
        {
            this._response = response;
        }
    }
}
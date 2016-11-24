using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
using Newtonsoft.Json; 

namespace OneApp.Common.WebServices.Models
{
    public abstract class BaseResponse
    {

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
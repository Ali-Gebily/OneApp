using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using OneApp.Common.WebServices.Models;

namespace OneApp.Common.WebServices.Controllers
{
    public class BaseApiController : ApiController
    {
        protected string UserId
        {
            get
            { return User.Identity.GetUserId(); }
        }

    }
}

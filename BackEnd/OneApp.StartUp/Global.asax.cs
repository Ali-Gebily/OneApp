using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;
using OneApp.Common.WebServices.Managers;

namespace OneApp.StartUp
{
    /// <summary>
    /// No code will be set here, just we need to force this dll to be loaded so that startup class runs
    /// </summary>
    public class Global : OneApp.Common.WebServices.Global
    {

    }
}
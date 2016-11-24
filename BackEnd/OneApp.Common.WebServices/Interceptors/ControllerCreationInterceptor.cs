using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.DynamicProxy;
using OneApp.Common.Core.Managers;
using OneApp.Common.WebServices.Controllers;
using OneApp.Common.WebServices.Managers;

namespace OneApp.Common.WebServices.Interceptors
{
    /// <summary>
    /// Used to handle exception while initializing controller
    /// </summary>
    public class ControllerCreationInterceptor : Ninject.Extensions.Interception.IInterceptor
    {

        public void Intercept(Ninject.Extensions.Interception.IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                ExceptionManager.Instance.Process(ex);          
            }
        }
    }
}
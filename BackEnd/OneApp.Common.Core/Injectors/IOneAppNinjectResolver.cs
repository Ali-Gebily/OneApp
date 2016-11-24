using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;

namespace OneApp.Common.Core.Injectors
{
    public interface IOneAppNinjectResolver
    {
        void RegisterServices(IOneAppKernel kernel);
     

    }
}
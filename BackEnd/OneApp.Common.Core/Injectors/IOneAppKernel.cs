using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace OneApp.Common.Core.Injectors
{
    public interface IOneAppKernel 
    {
          void BindConcerteToAbstact<IT, CT>()
     where CT : IT;

    }
}

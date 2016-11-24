using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Syntax;

namespace OneApp.Common.Core.Injectors
{
    public class OneAppKernel : IOneAppKernel
    {
        IKernel _kernel;
        public OneAppKernel(IKernel kernel)
        {
            _kernel = kernel;
        }
        public void BindConcerteToAbstact<IT, CT>()
            where CT : IT
        {
            _kernel.Bind<IT>().To<CT>();
        }

    }
}

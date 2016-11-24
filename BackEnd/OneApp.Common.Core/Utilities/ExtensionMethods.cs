using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneApp.Common.Core.Utilities
{
    public static class ExtensionMethods
    {

        public static Exception GetMostInnerException(this Exception ex)
        {
            var inner = ex;
            while (inner != null && inner.InnerException != null)
            {
                inner = inner.InnerException;
            }
            return inner;
        }

    }
}

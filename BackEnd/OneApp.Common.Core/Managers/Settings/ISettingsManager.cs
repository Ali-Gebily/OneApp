using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneApp.Common.Core.Managers.Settings
{
    public interface ISettingsManager
    {
        string GetValue(string key); 
    }
}

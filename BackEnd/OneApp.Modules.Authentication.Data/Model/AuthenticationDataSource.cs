using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneApp.Modules.Authentication.Data.Model
{
    public enum AuthenticationDataSource
    {
        Mock = 0,
        SqlServer = 1,
        MongoDb = 2,
        LDAP = 3
    }
}

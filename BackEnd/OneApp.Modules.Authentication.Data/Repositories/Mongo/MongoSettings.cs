using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using OneApp.Common.Core.Managers.Settings; 
namespace OneApp.Modules.Authentication.Data.Repositories.Mongo
{
    class MongoSettings
    {
        internal static class Collections
        {
            internal const string AspNetUsers = "AspNetUsers";
            internal const string AspNetRoles = "AspNetRoles";
            internal const string AspNetUserRoles = "AspNetUserRoles";
            internal const string AspNetUserClaims = "AspNetUserClaims";
            internal const string AspNetUserLogins = "AspNetUserLogins";
        }
       
    }
}

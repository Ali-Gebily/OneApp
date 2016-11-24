using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace OneApp.Modules.Authentication.Data.Repositories.Base
{
    interface IUniqueEntity
    {
        ObjectId Id { get; set; }
    }
}

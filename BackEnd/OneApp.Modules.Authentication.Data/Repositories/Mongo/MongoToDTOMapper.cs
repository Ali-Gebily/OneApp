using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OneApp.Modules.Authentication.Data.Model;

namespace OneApp.Modules.Authentication.Data.Repositories.Mongo
{
    class MongoModelToDTOMapper
    {
        static MapperConfiguration config = null;

        static MongoModelToDTOMapper()
        {

            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MongoIdentityUser, IdentityUserDTO>();
                cfg.CreateMap<MongoIdentityRole, IdentityRoleDTO>();
                cfg.CreateMap<MongoIdentityUserRole, IdentityUserRoleDTO>();
                cfg.CreateMap<MongoIdentityUserClaim, IdentityUserClaimDTO>();
                cfg.CreateMap<MongoIdentityUserLogin, IdentityUserLoginDTO>();

            });
            config.AssertConfigurationIsValid();

        }
        public static IdentityUserDTO Map(MongoIdentityUser source)
        {
            var mapper = config.CreateMapper();
            return mapper.Map<MongoIdentityUser, IdentityUserDTO>(source);

        }
    }
}

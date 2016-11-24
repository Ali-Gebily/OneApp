using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OneApp.Modules.Authentication.Data.Model;

namespace OneApp.Modules.Authentication.Data.Repositories.Mock
{
    class MemoryModelToDTOMapper
    {
        static MapperConfiguration config = null;

        static MemoryModelToDTOMapper()
        {

            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MockIdentityUser, IdentityUserDTO>();
                cfg.CreateMap<MockIdentityRole, IdentityRoleDTO>();
                cfg.CreateMap<MockIdentityUserRole, IdentityUserRoleDTO>();
                cfg.CreateMap<MockIdentityUserClaim, IdentityUserClaimDTO>();
                cfg.CreateMap<MockIdentityUserLogin, IdentityUserLoginDTO>();

            });
            config.AssertConfigurationIsValid();

        }
        public static IdentityUserDTO Map(MockIdentityUser source)
        {
            var mapper = config.CreateMapper();
            return mapper.Map<MockIdentityUser, IdentityUserDTO>(source);

        }
    }
}

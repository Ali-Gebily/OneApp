using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using OneApp.Modules.Authentication.Data.Model;

namespace OneApp.Modules.Authentication.Data.Repositories.EntityFramework
{
    internal class EFToDTOMapper
    {
        static MapperConfiguration config = null;

        static EFToDTOMapper()
        {

        config = new MapperConfiguration(cfg =>
       {
           cfg.CreateMap<IdentityUser, IdentityUserDTO>();
           cfg.CreateMap<IdentityRole, IdentityRoleDTO>();
           cfg.CreateMap<IdentityUserRole, IdentityUserRoleDTO>();
           cfg.CreateMap<IdentityUserClaim, IdentityUserClaimDTO>();
           cfg.CreateMap<IdentityUserLogin, IdentityUserLoginDTO>();

       });
            config.AssertConfigurationIsValid();

        }
        public static IdentityUserDTO Map(IdentityUser source)
        { 
            var mapper = config.CreateMapper();
            return mapper.Map<IdentityUser, IdentityUserDTO>(source);

        }
        public static IdentityRoleDTO Map(IdentityRole source)
        {
            var mapper = config.CreateMapper();
            return mapper.Map<IdentityRole, IdentityRoleDTO>(source);

        }
    }
}

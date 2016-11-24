using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security.OAuth;
using OneApp.Common.WebServices.App_Start;
using OneApp.Modules.Authentication.Data.Model;
using OneApp.Modules.Authentication.Data.Repositories;

namespace OneApp.Modules.Authentication.WebServices.Providers
{
    public class OneAppAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        //client means origin 
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
        
            using (IAuthenticationRepository _repo = NinjectWebCommon.Load<IAuthenticationRepository>())
            {
                IdentityUserDTO user = await _repo.FindUser(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("The username or password is incorrect.", null);
                    return;
                }

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));


                context.Validated(identity);
            }

        }

    }
}
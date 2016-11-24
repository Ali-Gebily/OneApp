using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using OneApp.Common.WebServices;
using OneApp.Modules.Authentication.WebServices.Providers;
using Owin;

[assembly: OwinStartup(typeof(OneApp.Modules.Authentication.WebServices.Startup))]
namespace OneApp.Modules.Authentication.WebServices
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            ConfigureOAuth(app);
            var config = WebApiConfig.Configuration;
            WebApiConfig.Register(config);

            app.UseWebApi(config);
        }
        void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(24),
                Provider = new OneAppAuthorizationServerProvider() 
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }

    }
}
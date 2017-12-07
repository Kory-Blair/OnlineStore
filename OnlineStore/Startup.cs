using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.Cookies;
using Owin;


[assembly: Microsoft.Owin.OwinStartup(typeof(CodingTemple.CodingCookware.Web.Startup))]

namespace CodingTemple.CodingCookware.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)

        {

            app.UseCookieAuthentication(new CookieAuthenticationOptions

            {

                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,

                LoginPath = new Microsoft.Owin.PathString("/Account/LogOn")

            });

            app.CreatePerOwinContext(() =>

            {

                UserStore<IdentityUser> store = new UserStore<IdentityUser>();

                UserManager<IdentityUser> manager = new UserManager<IdentityUser>(store);

                manager.UserTokenProvider = new EmailTokenProvider<IdentityUser>();

                manager.PasswordValidator = new PasswordValidator

                {

                    RequiredLength = 4,
                    RequireDigit = false,
                    RequireUppercase = false,
                    RequireLowercase = false,
                    RequireNonLetterOrDigit = false

                };
                //manager.EmailService = new OnlineStoreEmailService();

                return manager;

            });

        }
    }
}

    
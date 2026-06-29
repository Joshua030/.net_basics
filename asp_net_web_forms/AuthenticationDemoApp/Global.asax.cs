using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace AuthenticationDemoApp
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                string[] roles = new string[0];
                switch (User.Identity.Name)
                {
                    case "u1":
                        roles = new string[] { "r1", "r3" };
                        break;
                    case "u2":
                        roles = new string[] { "r2", "r3" };
                        break;
                    case "u3":
                        roles = new string[] { "r3" };
                        break;
                    default:
                        break;

                }
                Context.User = new System.Security.Principal.GenericPrincipal(User.Identity, roles);
            }

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
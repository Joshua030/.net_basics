using System;
using System.Web.Security;

namespace ProvidersDemoApp
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // Seed the demo roles once on startup. The role tables were created
            // automatically inside aspnetdb.mdf when the membership database was
            // first generated, so all we do here is insert the role rows.
            // CreateRole throws if the role already exists, so guard with RoleExists.
            foreach (var role in new[] { "r1", "r2", "r3" })
            {
                if (!Roles.RoleExists(role))
                {
                    Roles.CreateRole(role);
                }
            }
        }
    }
}

using System;
using System.Web.Security;

namespace ProvidersDemoApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Only show the roles panel to a logged-in user.
            if (User.Identity.IsAuthenticated)
            {
                RolesPanel.Visible = true;
                LitUser.Text = User.Identity.Name;
                BindRoles();
            }
        }

        private void BindRoles()
        {
            // Roles.GetRolesForUser() reads aspnet_UsersInRoles for the current user.
            string[] roles = Roles.GetRolesForUser();
            LitRoles.Text = roles.Length > 0 ? string.Join(", ", roles) : "(none)";
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            string role = DdlRoles.SelectedValue;
            if (!Roles.IsUserInRole(role))
            {
                Roles.AddUserToRole(User.Identity.Name, role);
            }
            BindRoles();
        }

        protected void BtnRemove_Click(object sender, EventArgs e)
        {
            string role = DdlRoles.SelectedValue;
            if (Roles.IsUserInRole(role))
            {
                Roles.RemoveUserFromRole(User.Identity.Name, role);
            }
            BindRoles();
        }
    }
}

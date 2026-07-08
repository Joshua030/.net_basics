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

        protected void BtnCreateUser_Click(object sender, EventArgs e)
        {
            // Membership Methos has all the function to find out about whatever property of user.
            MembershipCreateStatus status;
            MembershipUser user = Membership.CreateUser("user_04", "user_04", "user_04@test.com",
                      null, null, true, out status);
            Response.Write("Created sucessfully " + status.HasFlag(MembershipCreateStatus.Success).ToString());
            Response.Write(status.ToString());
        }

        protected void BtnCreateRole_Click(object sender, EventArgs e)
        {
            Roles.CreateRole(TxtRole.Text);
        }

        protected void BtnBindRole_Click(object sender, EventArgs e)
        {
            Roles.AddUserToRole("user_04", "r4");
        }
    }
}

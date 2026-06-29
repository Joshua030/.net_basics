using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AuthenticationDemoApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                BtnLogin.Text = "Logout";
                LtlUsername.Text = User.Identity.Name;
            }
            else
            {
                BtnLogin.Text = "Login";
                LtlUsername.Text = "Guest";
            }
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                System.Web.Security.FormsAuthentication.SignOut();
                BtnLogin.Text = "Login";
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}
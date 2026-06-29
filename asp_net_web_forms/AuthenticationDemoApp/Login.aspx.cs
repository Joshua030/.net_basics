using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace AuthenticationDemoApp
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = TxtLogin.Text;
            string password = TxtPassword.Text;
            bool isAuthenticated = FormsAuthentication.Authenticate(username, password);

            if (isAuthenticated)
            {
                FormsAuthentication.SetAuthCookie(username, ChkRememberMe.Checked);
                string returnUrl = Request.QueryString["ReturnUrl"] ?? "~/";
                Response.Redirect(returnUrl);
            }
            else
            {
                LblError.Text = "invalid username or password";
            }
        }
    }
}
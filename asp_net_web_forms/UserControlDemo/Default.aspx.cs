using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserControlDemo
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void UcLoginAuthenticated(object sender, AuthenticatedEventArgs e)
        {
            string username = e.AuthenticatedUserName;
            Response.Redirect("WelcomeAuthenticatedUser.aspx?username=" + username);
        }
    }
}
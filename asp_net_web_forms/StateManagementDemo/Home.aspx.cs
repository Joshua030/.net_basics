using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StateManagementDemo
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["username"] == null)
            {
                LblLoginStatus.Text = "Please login";
                LnkLoging.Text = "Login";
                return;
            }

            LblLoginStatus.Text = "Welcome "  + Session["username"].ToString();
            LnkLoging.Text = "Logout";

        }

        protected void LnkLoging_Click(object sender, EventArgs e)
        {
            if(Session["username"] == null) {
                Response.Redirect("LoginSession.aspx");
                return;
            }

            Session.Remove("username");
            Response.Redirect(Request.Path);
            
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StateManagementDemo
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string userName = TxtUsername.Text;
            string password = TxtPassword.Text;

          
            if (userName == password)
            {
                HttpCookie cookie= new HttpCookie("Authcookie")
                {
                    Value = userName,
                    Expires = ChkRemember.Checked ? DateTime.Now.AddDays(7) : DateTime.MinValue,
                    Path = Request.ApplicationPath
                };
                Response.Cookies.Add(cookie);

                string returnUrl = Request.QueryString["returnUrl"] ?? "~/Home.aspx";
                Response.Redirect(returnUrl);
            } else
            {
                LblError.Text = "Invalid username or password";
            }

         
           

        }
    }
}
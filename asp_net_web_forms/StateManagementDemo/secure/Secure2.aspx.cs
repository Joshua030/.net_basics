using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StateManagementDemo.secure
{
    public partial class Secure2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["Authcookie"];
            if (cookie == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                Response.Write("welcome " + cookie.Value);
            }
        }
    }
}
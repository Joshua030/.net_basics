using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StateManagementDemo
{
    public partial class Page1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnNext_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = new HttpCookie("name")
            {
                Value = TxtName.Text,
                Path = Request.ApplicationPath
            };
            Response.Cookies.Add(cookie);

            Response.Cookies.Add(new HttpCookie("age") { Value = TxtAge.Text, Path = Request.ApplicationPath });

            Response.Redirect("Page2.aspx");
        }
    }
}
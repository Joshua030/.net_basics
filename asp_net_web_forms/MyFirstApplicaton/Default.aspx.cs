using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyFirstApplicaton.App_Code;

namespace MyFirstApplicaton
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(Class1.SayHello());
            TextBox1.Text = "Load";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            TextBox1.Text = "Demo";
        }

        protected void BtnRedirect_Click(object sender, EventArgs e)
        {
            // Redirect with a event
            Response.Redirect("Default2.aspx");
        }

        protected void BtnTransfer_Click(object sender, EventArgs e)
        {
            // Transfer with a event
            // Code is executed twice in the case of transfer, once for the source page and once for the destination page (Refresh).
            Server.Transfer("Default2.aspx");
        }
    }
}
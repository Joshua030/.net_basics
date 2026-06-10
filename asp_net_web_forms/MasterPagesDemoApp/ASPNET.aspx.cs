using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MasterPagesDemoApp
{
    public partial class ASPNET : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnDemo_Click(object sender, EventArgs e)
        {
            Master.MasterPageLabel = "Button Clicked in ASP.NET";
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConfigurationDemoApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Page.MetaKeywords = "asp,net,C#";
            Page.MetaDescription = "this is an asp.net site that host asp.net tutorials";
        }

        // handle global page error 
        protected void Page_Error(object sender, EventArgs e)
        {
            //Exception exception = Context.Error;
            //if(exception is ApplicationException)
            //{
            //    Response.Write(exception.Message);
            //    Context.ClearError();
            //}
         
        }

        protected void BtnError_Click(object sender, EventArgs e)
        {
            throw new ApplicationException();
        }
    }
}
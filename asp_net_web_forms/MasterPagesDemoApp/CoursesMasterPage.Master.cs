using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MasterPagesDemoApp
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnDemo_Click(object sender, EventArgs e)
        {
            LblInMaster.Text = "Button Clicked in Master Page";
        }

        public string MasterPageLabel
        {
            get { return LblInMaster.Text; }
            set { LblInMaster.Text = value; }
        }
    }
}
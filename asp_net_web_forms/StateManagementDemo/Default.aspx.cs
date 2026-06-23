using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StateManagementDemo
{
    public partial class Default : System.Web.UI.Page
    {
        // Instance counter for each page instance
        int Counter = 0;
        // Shared counter across all instances of the page
        static int SharedCounter = 0;
        //static string action;
        protected void Page_Load(object sender, EventArgs e)
        {
       
            LblCounter.Text = (++Counter).ToString();
            LblSharedCounter.Text = (++SharedCounter).ToString();
          
        }

        protected void BtnNew_Click(object sender, EventArgs e)
        {
            BtnSave.Visible = true;
            BtnEdit.Visible = BtnNew.Visible = false;
            // ViewState is used to store the action state for the current page instance
            ViewState["action"] = "New";

        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            ViewState["action"] = "Edit";
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            /*
         if(action == "New")
            {
                Response.Write("New was clicked");
            } else
            {
                Response.Write("Edit was clicked");
            }
            */
        }
    }
}
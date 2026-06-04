using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyFirstApplicaton
{
    public partial class Default2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void rbnColor_CheckedChanged(object sender, EventArgs e)
        {

            RadioButton rbn = (RadioButton)sender;
            lblDemo.ForeColor = System.Drawing.Color.FromName(rbn.Text);
            //            if (rbnRed.Checked)
            //            {
            //                lblDemo.ForeColor = System.Drawing.Color.Red;

            //            }
            //            else if (rbnGreen.Checked)
            //            {
            //                lblDemo.ForeColor = System.Drawing.Color.Green;
            //            }
            //            else
            //            {

            //lblDemo.ForeColor = System.Drawing.Color.Blue;
            //            }
        }

      

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}
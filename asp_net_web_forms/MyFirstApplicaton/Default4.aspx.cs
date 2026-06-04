using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyFirstApplicaton
{
    public partial class Default4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack) // to avoid adding items to listbox on every postback
            {
                for (int i = 0; i < 5; i++)
                {
                    ListItem li = new ListItem();
                    li.Text = "Item " + i;
                    li.Value = i.ToString();
                    lsbLeft.Items.Add(li);
                }
            }
         
        }

        protected void btnMoveToRight_Click(object sender, EventArgs e)
        {
            ListItem li = lsbLeft.SelectedItem;
            lsbLeft.Items.Remove(li);
            li.Selected = false;
            lsbRight.Items.Add(li);
        }

        protected void btnMoveToLeft_Click(object sender, EventArgs e)
        {
            ListItem lr = lsbRight.SelectedItem;
            lsbRight.Items.Remove(lr);
            lr.Selected = false;
            lsbLeft.Items.Add(lr);
        }
    }
}
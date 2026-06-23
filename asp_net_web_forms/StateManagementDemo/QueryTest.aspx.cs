using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StateManagementDemo
{
    public partial class QueryTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            // We need send the encoded value to parse symbols
            string parsedText = Server.UrlEncode(TxtSearch.Text);
            Response.Redirect("~/WebForm1.aspx?search=" + parsedText);

        }

        protected void BtnTransfer_Click(object sender, EventArgs e)
        {
            // TRanfer using context with request scope
            Context.Items["demo"] = TxtSearch.Text;
            Server.Transfer("WebForm1.aspx");
        }
    }
}
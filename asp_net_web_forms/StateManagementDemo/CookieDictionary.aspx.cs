using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StateManagementDemo
{
    public partial class CookieDictionary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnAddToDictionary_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["cd"] ?? new HttpCookie("cd");
     
            cookie.Expires = DateTime.MaxValue; 
            cookie.Values[TxtSubKey.Text] = TxtValue.Text;
            Response.Cookies.Add(cookie);
        }

        protected void BtnShowDictionary_Click(object sender, EventArgs e)
        {
            LblValue.Text = "";
            HttpCookie cookie = Request.Cookies["cd"];

            if (cookie == null) return;

            foreach (var sk in cookie.Values)
            {
               LblValue.Text += sk + ":" + cookie.Values[(string)sk] + "<br />"; 
            }
        }
    }
}
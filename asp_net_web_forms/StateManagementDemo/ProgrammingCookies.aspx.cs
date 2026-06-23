using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StateManagementDemo
{
    public partial class ProgrammingCookies : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSetCookie_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = new HttpCookie(txtKey.Text);
            cookie.Value = txtValue.Text;
            cookie.Path = Request.ApplicationPath;

            if (chkPersistance.Checked)
            {
                cookie.Expires = DateTime.MaxValue;
                cookie.Secure = chkSecured.Checked;
            }

            Response.Cookies.Add(cookie);

        }

        protected void btnGetCookie_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies[txtKey.Text];

            if(cookie == null)
            {
                txtValue.Text = "----";
            } else
            {
                txtValue.Text = cookie.Value;
            }

        }

        protected void btnDestroyCookie_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = new HttpCookie(txtKey.Text);
            cookie.Path = Request.ApplicationPath;
            cookie.Expires = DateTime.Today.AddDays(-1);
            Response.Cookies.Add(cookie);

        }

        protected void btnGetAllCookies_Click(object sender, EventArgs e)
        {

            lblValue.Text = "";
            foreach (string key in Request.Cookies)
            {
                lblValue.Text += key + ": " + Request.Cookies[key].Value + "<br />";
            }

        }
    }
}
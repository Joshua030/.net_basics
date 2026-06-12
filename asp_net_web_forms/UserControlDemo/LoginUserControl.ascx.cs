using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserControlDemo
{
    public partial class LoginUserControl : System.Web.UI.UserControl
    {

        public bool IsAuthenticated { get; set; }
        public string ReturnUrl { get; set; }
        public string InvalidUserMessage { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = TxtUsername.Text;
            string password = TxtPassword.Text;

            // For demonstration purposes, we'll just check if the username and password are "admin"

            if(username == "admin" && password == "admin")
            {
                IsAuthenticated = true;
                LblMessage.Text = "Login successful!";
            }
            else
            {
                IsAuthenticated = false;
                LblMessage.Text = "Invalid username or password.";
            }

            if (IsAuthenticated)
            {
                if (String.IsNullOrEmpty(ReturnUrl))
                {
                    Response.Redirect("~/");

                }
                else
                {
                    Response.Redirect(ReturnUrl);
                }
            }

            if (!IsAuthenticated)
            {
                if (String.IsNullOrEmpty(InvalidUserMessage))
                {
                    LblErrorText = "Invalid username or password.";

                }
                else
                {
                    LblErrorText = InvalidUserMessage;
                }
            }
        }
    }
}
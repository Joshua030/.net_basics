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
        // prop with default event handler
        //public event EventHandler Authenticated;

        public event AuthenticatedHandler Authenticated;
        public bool IsAuthenticated { get; set; }
        public string ReturnUrl { get; set; }
        public string InvalidUserMessage { get; set; }

        public string UsernameLabel {

            get {
                return LtrUsername.Text;
            }
            set { 
              LtrUsername.Text = value;
            } 
        }

        public string PasswordLabel
        {
            get
            {
                return LtrPassword.Text;
            }
            set
            {
                LtrPassword.Text = value;
            }
        }

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

            // Raise the Authenticated event
            if (IsAuthenticated) {
                if (Authenticated != null)
            {
                    AuthenticatedEventArgs args = new AuthenticatedEventArgs()
                    {
                        AuthenticatedUserName = username
                    };
                    Authenticated(this, args);
            }
            }



            // Check if the user is authenticated and redirect accordingly

            /* if (IsAuthenticated)
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
            */

            if (!IsAuthenticated)
            {
                if (String.IsNullOrEmpty(InvalidUserMessage))
                {
                    LblErrorText.Text = "Invalid username or password.";

                }
                else
                {
                    LblErrorText.Text = InvalidUserMessage;
                }
            }
        }
    }

    public delegate void AuthenticatedHandler(object sender, AuthenticatedEventArgs e);

    public class AuthenticatedEventArgs : EventArgs
    {
        public string AuthenticatedUserName { get; set; }
    }
}
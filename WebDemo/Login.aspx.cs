namespace WebDemo
{
    using System;
    using System.Web.Configuration;
    using Yfy.Api;

    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var clientId = WebConfigurationManager.AppSettings["ClientId"];
            var clientSecret = WebConfigurationManager.AppSettings["ClientSecret"];
            YfySystem.Init(clientId, clientSecret);
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            string userName = userNameTextbox.Text;
            Session["UserName"] = userName;

            Response.Redirect("~/Home.aspx");
        }
    }
}
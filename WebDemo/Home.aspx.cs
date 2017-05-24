namespace WebDemo
{
    using System;
    using Yfy.Api;
    using Yfy.Api.Oauth;

    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var nowUserName = Session["UserName"];
            if (nowUserName != null)
            {
                userNameLabel.InnerText = Session["UserName"].ToString();

                var oauthToken = Session["Token"];
                if (oauthToken != null)
                {
                    linkBtn.Text = "View your fangcloud account";
                    linkBtn.Click -= this.linkBtn_Click;
                    linkBtn.Click += this.linkBtn_view_Click;
                }
            }
            else
            {
                Response.Redirect("/Login.aspx");
            }
        }

        protected void linkBtn_Click(object sender, EventArgs e)
        {
            string state = OAuthHelper.InitState();
            Session["State"] = state;

            string url = OAuthHelper.GetAuthorizeUrl(System.Web.Configuration.WebConfigurationManager.AppSettings["RedirectUrl"], state);
            Response.Redirect(url);
        }

        protected void linkBtn_view_Click(object sender, EventArgs e)
        {
            var userName = Session["UserName"];
            var token = Session["Token"] as YfyAuthtoken;

            var fc = new YfyClient(token.AccessToken, token.RefreshToken);
            var user = fc.User.Info();

            userInfoLabel.Attributes.Remove("hidden");
            userInfoLabel.InnerText = user.ToString();
        }

        protected void logoutBtn_Click(object sender, EventArgs e)
        {
            var userName = Session["UserName"];
            Session.Remove("Token");
            Session.Remove("UserName");
            Application.Remove(userName.ToString());

            linkBtn.Click -= this.linkBtn_view_Click;
            linkBtn.Click += this.linkBtn_Click;

            Response.Redirect("/Login.aspx");
        }
    }
}
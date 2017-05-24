namespace WebDemo
{
    using System;
    using System.Linq;

    using Yfy.Api;
    using Yfy.Api.Oauth;

    public partial class Oauth : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var queryArgs = Request.QueryString;

            if (queryArgs.Count > 0)
            {
                string[] keys = queryArgs.AllKeys;

                if (keys.Contains("code") && keys.Contains("state"))
                {
                    string code = queryArgs.GetValues("code")[0];
                    string state = queryArgs.GetValues("state")[0];

                    if (state != Session["State"].ToString())
                    {
                        Session.Remove("State");
                        Response.Redirect("/Login.aspx");
                    }
                    else
                    {
                        var token = OAuthHelper.GetOAuthTokenByCode(code, System.Web.Configuration.WebConfigurationManager.AppSettings["RedirectUrl"]);

                        Session["Token"] = token;
                        Session.Remove("State");
                        Response.Redirect("/Home.aspx");
                    }
                }
            }

            Response.Redirect("/Home.aspx");
        }
    }
}
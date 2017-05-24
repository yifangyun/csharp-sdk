namespace Yfy.Api
{
    internal class YfyClientInfo
    {
        public static string ClientId { get; set; } = "";

        public static string ClientSecret { get; set; } = "";

        public static string RedirectUri { get; set; } = "";

        public static void Init(string clientId, string clientSecret, string redirectUri = "")
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            RedirectUri = redirectUri;
        }
    }
}

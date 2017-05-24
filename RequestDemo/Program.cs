namespace RequestDemo
{
    using System;
    using Yfy.Api;

    class Program
    {
        static void Main(string[] args)
        {
            // init yfy system info
            YfySystem.Init(ClientInfo.ClientId, ClientInfo.ClientSecret);

            // get fangcloud api client
            var fc = new YfyClient(ClientInfo.AccessToken, ClientInfo.RefreshToken);

            // use user to get user info
            var user = fc.User.Info();

            // output
            Console.WriteLine(user);

            Console.WriteLine("success");
        }
    }

    public class ClientInfo
    {
        public static string AccessToken = "Your Access Token";

        public static string RefreshToken = "Your Refresh Token";

        public const string ClientId = "Your Client Id";

        public const string ClientSecret = "Your Client Secret";
    }
}

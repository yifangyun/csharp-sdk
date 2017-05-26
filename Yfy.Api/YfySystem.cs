namespace Yfy.Api
{
    using System;
    using System.Reflection;

    /// <summary>
    /// 亿方云C#SDK系统
    /// </summary>
    public class YfySystem
    {
        internal static string CallerImageRuntimeVersion;

        /// <summary>
        /// SDK初始化
        /// </summary>
        /// <param name="clientId">亿方云上申请的客户端id</param>
        /// <param name="clientSecret">亿方云上申请的客户端secret</param>
        /// <param name="redirectUri">亿方云上申请的客户端重定向Uri</param>
        public static void Init(string clientId, string clientSecret, string redirectUri = "")
        {
            //Init Yfy System
            YfyClientInfo.Init(clientId, clientSecret, redirectUri);

            //Init User Agent
            Assembly assembly = Assembly.GetExecutingAssembly();
            YfyRequestHandler.UserAgent = string.Format(YfyClientInfo.ClientId + "OfficialFangcloudCSharpSDK/{0}", assembly.GetName().Version.ToString());

            //Get Caller ImageRuntimeVersion
            Assembly callerAssemble = Assembly.GetCallingAssembly();
            YfySystem.CallerImageRuntimeVersion = callerAssemble.ImageRuntimeVersion;

            //Init Test Environment
            var IsTestEnvironment = Environment.GetEnvironmentVariable("FangcloudTest");

            if (IsTestEnvironment != null)
            {
                UriHelper.ApiHost = "https://platform.fangcloud.net/";
                UriHelper.OAuthHost = "https://oauth-server.fangcloud.net/";
            }
        }
    }
}

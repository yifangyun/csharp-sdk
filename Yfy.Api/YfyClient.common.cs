namespace Yfy.Api
{
    using Yfy.Api.Oauth;

    /// <summary>
    /// 亿方云C#SDK客户端对象
    /// </summary>
    public partial class YfyClient
    {
        private readonly ITransport _transport;

        internal YfyClient(ITransport transport)
        {
            this._transport = transport;
            this.InitRouters(transport);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config">客户端配置</param>
        public YfyClient(YfyClientConfig config)
            : this(new YfyRequestHandler(config))
        {
            
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="oauth2AccessToken">通过Oauth协议得到的用户相关的AssessToken</param>
        public YfyClient(string oauth2AccessToken)
            :this(new YfyClientConfig(oauth2AccessToken))
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="oauth2AccessToken">通过Oauth协议得到的用户相关的AssessToken</param>
        /// <param name="oauth2RefreshToken">通过Oauth协议得到的用户相关的RefreshToken</param>
        public YfyClient(string oauth2AccessToken, string oauth2RefreshToken)
            : this(new YfyClientConfig(oauth2AccessToken, oauth2RefreshToken))
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="authToken">YfyAuthtoken对象</param>
        public YfyClient(YfyAuthtoken authToken)
            :this(authToken.AccessToken, authToken.RefreshToken)
        {

        }

        private YfyClientConfig GetYfyClientConfig()
        {
            return (this._transport as YfyRequestHandler).YfyClientConfig;
        }
    }
}

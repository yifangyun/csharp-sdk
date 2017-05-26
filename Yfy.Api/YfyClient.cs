namespace Yfy.Api
{
    using System;

    using Yfy.Api.Users;
    using Yfy.Api.Files;
    using Yfy.Api.Folders;
    using Yfy.Api.Trash;
    using Yfy.Api.ShareLink;
    using Yfy.Api.Collab;
    using Yfy.Api.Comment;
    using Yfy.Api.Items;

    using Newtonsoft.Json;

    public partial class YfyClient
    {
        /// <summary>
        /// 用户相关api
        /// </summary>
        public UserRouter User { get; private set; }

        /// <summary>
        /// 文件相关api
        /// </summary>
        public FileRouter File { get; private set; }

        /// <summary>
        /// 文件夹相关api
        /// </summary>
        public FolderRouter Folder { get; private set; }

        /// <summary>
        /// 文件（文件夹）相关api
        /// </summary>
        public ItemRouter Item { get; private set; }

        /// <summary>
        /// 回收站相关api
        /// </summary>
        public TrashRouter Trash { get; private set; }

        /// <summary>
        /// 分享链接相关api
        /// </summary>
        public ShareLinkRouter ShareLink { get; private set; }

        /// <summary>
        /// 评论相关api
        /// </summary>
        public CommentRouter Comment { get; private set; }

        /// <summary>
        /// 协作相关api
        /// </summary>
        public CollabRouter Collab { get; private set; }

        internal void InitRouters(ITransport transport)
        {
            this.User = new UserRouter(transport);
            this.File = new FileRouter(transport);
            this.Folder = new FolderRouter(transport);
            this.Item = new ItemRouter(transport);
            this.Trash = new TrashRouter(transport);
            this.ShareLink = new ShareLinkRouter(transport);
            this.Comment = new CommentRouter(transport);
            this.Collab = new CollabRouter(transport);

            //约定
            //Post请求参数：
            //默认值为null的忽略
            //默认值不为null的需要序列化
            JsonSerializerSettings setting = new JsonSerializerSettings();
            JsonConvert.DefaultSettings = new Func<JsonSerializerSettings>(() =>
            {
                setting.NullValueHandling = NullValueHandling.Ignore;
                setting.DefaultValueHandling = DefaultValueHandling.Include;

                return setting;
            });
        }

        /// <summary>
        /// 设置当AccessToken刷新时的回调函数
        /// </summary>
        /// <param name="register">回调函数</param>
        public void SetTokenRegister(IYfyTokenRegister register)
        {
            GetYfyClientConfig().TokenRegister = register;
        }

        /// <summary>
        /// 设置SDK中Http请求的time out
        /// </summary>
        /// <param name="timeout">time out</param>
        public void SetTimeout(int timeout)
        {
            GetYfyClientConfig().HttpConfig.Timeout = timeout;
        }

        /// <summary>
        /// 设置SDK中Http请求的最大重试次数
        /// </summary>
        /// <param name="maxRetries">最大重试次数</param>
        public void SetMaxRetries(int maxRetries)
        {
            GetYfyClientConfig().HttpConfig.MaxRetries = maxRetries;
        }

        /// <summary>
        /// 设置SDK中Http请求的代理
        /// </summary>
        /// <param name="proxy">代理设置</param>
        public void SetProxy(System.Net.WebProxy proxy)
        {
            GetYfyClientConfig().HttpConfig.Proxy = proxy;
        }

        /// <summary>
        /// 设置SDK中的通过Oauth协议得到的用户相关的AssessToken
        /// </summary>
        /// <param name="oauth2AccessToken">AssessToken</param>
        public void SetOauth2AccessToken(string oauth2AccessToken)
        {
            GetYfyClientConfig().Oauth2AccessToken = oauth2AccessToken;
        }

        /// <summary>
        /// 设置SDK中的通过Oauth协议得到的用户相关的RefreshToken
        /// </summary>
        /// <param name="oauth2RefresToken">RefreshToken</param>
        public void SetOauth2RefreshToken(string oauth2RefresToken)
        {
            GetYfyClientConfig().Oauth2RefreshToken = oauth2RefresToken;
        }

        /// <summary>
        /// 获得当前客户端使用的AssessToken
        /// </summary>
        /// <returns>当前的AssessToken字符串</returns>
        public string GetOauth2AccessToken()
        {
            return GetYfyClientConfig().Oauth2AccessToken;
        }
    }
}
    
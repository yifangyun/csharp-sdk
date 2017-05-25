namespace Yfy.Api.Oauth
{
    using System;
    using System.Text;
    using System.Security.Cryptography;
    using System.Net;
    using Newtonsoft.Json;
    using System.IO;
    using Yfy.Api.Exceptions;

    /// <summary>
    /// Oauth相关的帮助类
    /// </summary>
    public static class OAuthHelper
    {
        /// <summary>
        /// 初始化Oauth相关的state
        /// </summary>
        /// <returns>返回随机的state值，长度固定为6位</returns>
        public static string InitState()
        {
            return BitConverter.ToString(
                        MD5.Create().ComputeHash(
                            Encoding.UTF8.GetBytes(DateTime.Now.ToString()))
                    ).Replace("-", string.Empty).ToLower().Substring(0, 6);
        }

        /// <summary>
        /// 获得Oauth授权模式授权的url
        /// </summary>
        /// <param name="redirectUri">oauth协议中的重定向url</param>
        /// <param name="state">oauth协议中的state</param>
        /// <returns>亿方云oauth认证过程中的授权url</returns>
        public static string GetAuthorizeUrl(string redirectUri, string state)
        {
            if (YfyClientInfo.ClientId == "" || YfyClientInfo.ClientSecret == "")
            {
                throw new YfyException("", "You need to init YfySystm first !");
            }

            return UriHelper.GetOAuthAuthorizeUri(YfyClientInfo.ClientId, redirectUri, state).ToString();
        }

        /// <summary>
        /// 获得基本的Http认证Header
        /// </summary>
        /// <returns>Http Header 字符串</returns>
        public static string GetBasicAuthrization()
        {
            if (YfyClientInfo.ClientId == "" || YfyClientInfo.ClientSecret == "")
            {
                throw new YfyException("", "You need to init YfySystem first !");
            }
            return "Basic " + Convert.ToBase64String(new UTF8Encoding(false).GetBytes(YfyClientInfo.ClientId + ":" + YfyClientInfo.ClientSecret));
        }

        /// <summary>
        /// 通过Oauth密码模式获得OauthToken
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>通用oauthtoken对象</returns>
        public static YfyAuthtoken GetOAuthTokenByPassword(string userName, string password)
        {
            var request = WebRequest.Create(UriHelper.GetOAuthPasswordUri(userName, password));
            request.Method = HttpMethod.Post;
            request.Headers.Add(HttpRequestHeader.Authorization, OAuthHelper.GetBasicAuthrization());

            try
            {
                var response = request.GetResponse() as HttpWebResponse;
                return JsonConvert.DeserializeObject<YfyAuthtoken>(new StreamReader(response.GetResponseStream(), new UTF8Encoding(false)).ReadToEnd());
            }
            catch (WebException we)
            {
                throw new YfyHttpException(we);
            }
        }

        /// <summary>
        /// 通过授权码模式获得OauthToken
        /// </summary>
        /// <param name="code">oauth服务器返回的code码</param>
        /// <param name="redirectUri">oauth协议中的重定向url</param>
        /// <returns>通用oauthtoken对象</returns>
        public static YfyAuthtoken GetOAuthTokenByCode(string code, string redirectUri)
        {
            var request = WebRequest.Create(UriHelper.GetOAuthTokenUri(code, redirectUri));
            request.Method = HttpMethod.Post;
            request.Headers.Add(HttpRequestHeader.Authorization, OAuthHelper.GetBasicAuthrization());

            try
            {
                var response = request.GetResponse() as HttpWebResponse;
                return JsonConvert.DeserializeObject<YfyAuthtoken>(new StreamReader(response.GetResponseStream(), new UTF8Encoding(false)).ReadToEnd());
            }
            catch (WebException we)
            {
                throw new YfyHttpException(we);
            }
        }

        /// <summary>
        /// 刷新OauthToken
        /// </summary>
        /// <param name="refreshToken">刷新所需要的RefreshToken</param>
        /// <returns>通用亿方云oauthtoken对象</returns>
        public static YfyAuthtoken RefreshOAuthToken(string refreshToken)
        {
            var request = WebRequest.Create(UriHelper.GetRefreshTokenUri(refreshToken)) as HttpWebRequest;
            request.Method = HttpMethod.Post;
            request.Headers.Add(HttpRequestHeader.Authorization, OAuthHelper.GetBasicAuthrization());
            try
            {
                var response = request.GetResponse() as HttpWebResponse;
                return JsonConvert.DeserializeObject<YfyAuthtoken>(new StreamReader(response.GetResponseStream(), new UTF8Encoding(false)).ReadToEnd());
            }
            catch (WebException we)
            {
                if ((we.Response as HttpWebResponse).StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new RefreshTokenExpiredException(refreshToken);
                }
                else
                {
                    throw new YfyHttpException(we);
                }
            }
        }

        /// <summary>
        /// 注销OauthToken
        /// </summary>
        /// <param name="accessToken">待注销的OauthToken</param>
        /// <returns>是否成功</returns>
        public static bool RevokeOAuthToken(string accessToken)
        {
            var request = WebRequest.Create(UriHelper.GetRevokeTokenUri(accessToken)) as HttpWebRequest;
            request.Method = HttpMethod.Post;
            request.Headers.Add(HttpRequestHeader.Authorization, OAuthHelper.GetBasicAuthrization());
            try
            {
                var response = request.GetResponse() as HttpWebResponse;
                return JsonConvert.DeserializeObject<YfySuccess>(new StreamReader(response.GetResponseStream(), new UTF8Encoding(false)).ReadToEnd()).Success;
            }
            catch (WebException we)
            {
                throw new YfyHttpException(we);
            }
        }
    }
}

namespace Yfy.Api.Oauth
{
    using System;
    using System.Text;
    using System.Security.Cryptography;
    using System.Net;
    using Newtonsoft.Json;
    using System.IO;
    using Yfy.Api.Exceptions;
    using System.Collections.Generic;
    using System.Security.Cryptography.X509Certificates;
    using Org.BouncyCastle.Crypto;
    using Org.BouncyCastle.OpenSsl;
    using Org.BouncyCastle.Security;
    using Org.BouncyCastle.Crypto.Parameters;

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
        /// <returns>通用Oauthtoken对象</returns>
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
        /// 通过Jwt获取OauthToken
        /// </summary>
        /// <param name="payload">Jwt中需要的内容</param>
        /// <param name="cert">X509Certificate2 对象, 包含私钥的证书文件</param>
        /// <returns>通用Oauthtoken对象</returns>
        public static YfyAuthtoken GetOAuthTokenByJwt(YfyJwtPayload payload, X509Certificate2 cert)
        {
            RSACryptoServiceProvider privateKey;
            var rsaCsp = cert.PrivateKey as RSACryptoServiceProvider;
            if (rsaCsp != null && rsaCsp.CspKeyContainerInfo.ProviderType == 1)
            {
                CspParameters csp = new CspParameters();

                csp.ProviderType = 24;
                csp.KeyContainerName = rsaCsp.CspKeyContainerInfo.KeyContainerName;
                csp.KeyNumber = (int)rsaCsp.CspKeyContainerInfo.KeyNumber;
                if (rsaCsp.CspKeyContainerInfo.MachineKeyStore)
                {
                    csp.Flags = CspProviderFlags.UseMachineKeyStore;
                }

                csp.Flags |= CspProviderFlags.UseExistingKey;
                privateKey = new RSACryptoServiceProvider(csp);
            }
            else
            {
                throw new ArgumentException(nameof(cert));
            }

            return _GetOAuthTokenByJwt(payload, privateKey);
        }

        /// <summary>
        /// 通过Jwt获取OauthToken
        /// </summary>
        /// <param name="payload">Jwt中需要的内容</param>
        /// <param name="keyPath">私钥路径。注意,私钥必须是pkcs1格式,不支持pkcs8</param>
        /// <param name="passwd">私钥密码</param>
        /// <returns>通用Oauthtoken对象</returns>
        public static YfyAuthtoken GetOAuthTokenByJwt(YfyJwtPayload payload, string keyPath, string passwd)
        {
            string pemString = new StreamReader(File.OpenRead(keyPath)).ReadToEnd();
            AsymmetricCipherKeyPair keyPair;

            using (StreamReader sr = new StreamReader(keyPath))
            {
                var passwdProvider = new RSAPasswdFinder(passwd);
                PemReader pr = new PemReader(sr, passwdProvider);
                keyPair = (AsymmetricCipherKeyPair)pr.ReadObject();
            }

            RSAParameters rsaParams = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)keyPair.Private);
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(rsaParams);

            return _GetOAuthTokenByJwt(payload, rsa);
        }

        private static YfyAuthtoken _GetOAuthTokenByJwt(YfyJwtPayload payload, RSACryptoServiceProvider privateKey)
        {
            string alg = Enum.GetName(typeof(JwtAlgorithms), payload.Alg);
            var jwtPayload = new Dictionary<string, object>() {
                { "yifangyun_sub_type", Enum.GetName(typeof(YfySubType), payload.SubType).ToLower() },
                { "sub", payload.Sub },
                { "exp", payload.Exp },
                { "iat", payload.Iat },
                { "jti", payload.Jti }
            };

            string token = Jose.JWT.Encode(
                jwtPayload,
                privateKey,
                (Jose.JwsAlgorithm)Enum.Parse(typeof(Jose.JwsAlgorithm), alg),
                new Dictionary<string, object>() { { "kid", payload.Kid } }
            );

            var request = WebRequest.Create(UriHelper.GetOAuthTokenJwtUri(token));
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

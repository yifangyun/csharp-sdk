namespace Yfy.Api
{
    using System;
    using System.Text;
    using System.IO;
    using System.Threading;
    using System.Net;
    using Yfy.Api.Exception;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Yfy.Api.Oauth;
    using System.Security.Cryptography;

    internal sealed class YfyRequestHandler : ITransport
    {
        public YfyClientConfig YfyClientConfig { get; set; }

        public static string UserAgent = "OfficalFangcloudCSharpSDK";

        private readonly string _apiVersion = "v2";

        private const string _Boundary = "--WebKitFormBoundary";

        public YfyRequestHandler(YfyClientConfig config)
        {
            this.YfyClientConfig = config;
        }

        internal enum RouteStyle
        {
            Rpc,
            Download,
            Upload,
        }

        TResponse ITransport.SendRpcRequest<TRequest, TResponse>(
            TRequest request,
            Uri uri,
            Func<TRequest, string> encoder,
            Func<string, TResponse> decoder)
        {
            string serializedArg = "";
            string httpMethod;

            if (request is GetArg)
            {
                httpMethod = HttpMethod.Get;
            }
            else
            {
                serializedArg = JsonConvert.SerializeObject(request);

                httpMethod = HttpMethod.Post;
            }
            
            string res = RequestJsonStringWithRetry(uri, RouteStyle.Rpc, httpMethod, serializedArg);

            return JsonConvert.DeserializeObject<TResponse>(res);
        }

        TResponse ITransport.SendUploadRequest<TRequest, TResponse>(
            TRequest request,
            Uri uri,
            Stream body,
            Func<TRequest, string> encoder,
            Func<string, TResponse> decoder)
        {
            var serializedArg = request as string;
            string res = this.RequestJsonStringWithRetry(uri, RouteStyle.Upload, HttpMethod.Post, serializedArg, body);

            return JsonConvert.DeserializeObject<TResponse>(res);
        }

        void ITransport.SendDownloadRequest(
            string request,
            Uri uri)
        {
            var serializedArg = request as string;
            string res =  this.RequestJsonStringWithRetry(uri, RouteStyle.Download, HttpMethod.Get, serializedArg);
        }

        private string RequestJsonStringWithRetry(
            Uri uri,
            RouteStyle routeStyle,
            string httpMethod,
            string requestArg = "",
            Stream body = null)
        {
            var attempt = 0;
            var maxRetries = this.YfyClientConfig.HttpConfig.MaxRetries;
            var r = new Random();

            if (routeStyle == RouteStyle.Upload)
            {
                if (body == null)
                {
                    throw new ArgumentNullException(nameof(body));
                }

                //为了重试机制，上传的流必须可以检索，否则无法重试
                if (!body.CanSeek)
                {
                    maxRetries = 0;
                }
            }

            while (true)
            {
                try
                {
                    return this.RequestJsonString(uri, routeStyle, httpMethod, requestArg, body);
                }
                catch (RateLimitException)
                {
                    throw ;
                }
                catch (RetryException re)
                {
                    if (++attempt > maxRetries)
                    {
                        throw new InternalServerException(re);
                    }
                }
                catch (TokenRefreshedException)
                {

                }

                var backoff = TimeSpan.FromSeconds(Math.Pow(2, attempt) * r.NextDouble());
                Thread.Sleep(backoff);

                if (body != null)
                {
                    body.Position = 0;
                }
            }
        }

        private string RequestJsonString(
            Uri uri,
            RouteStyle routeStyle,
            string httpMethod,
            string requestArg = "",
            Stream body = null)
        {
            HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;
            HttpWebResponse response;

            request.Method = httpMethod;
            request.Timeout = YfyClientConfig.HttpConfig.Timeout;

            request.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {YfyClientConfig.Oauth2AccessToken}");
            request.UserAgent = YfyRequestHandler.UserAgent;
            if (YfyClientConfig.HttpConfig.Proxy != null)
            {
                request.Proxy = YfyClientConfig.HttpConfig.Proxy;
            }

            switch (routeStyle)
            {
                case RouteStyle.Rpc:
                    {
                        request.ContentType = "application/json";
                        request.Accept = $"application/{this._apiVersion}+json";

                        if (requestArg != "")
                        {
                            using (Stream bodyStream = request.GetRequestStream())
                            {
                                var bodyBytes = new UTF8Encoding(false).GetBytes(requestArg);
                                bodyStream.Write(bodyBytes, 0, bodyBytes.Length);
                            }
                        }
                    }
                    break;
                case RouteStyle.Upload:
                    {
                        if (body == null)
                        {
                            throw new ArgumentNullException(nameof(body));
                        }
                        var boundary = YfyRequestHandler._Boundary + DateTime.Now.Ticks.ToString("x");
                        request.ContentType = "multipart/form-data; boundary=" + boundary;
                        request.SendChunked = true;
                        //上传时timeout设置为无限，防止上传大文件时出现问题
                        request.Timeout = Timeout.Infinite;
                        //重要，在上传大文件时该参数应该为false
                        request.AllowWriteStreamBuffering = false;

                        StringBuilder sb = new StringBuilder("--" + boundary);
                        sb.Append("\r\n");
                        sb.Append("Content-Disposition: form-data; name=\"file\"; filename=\"\"\r\n");
                        sb.Append("Content-Type: application/octet-stream");
                        sb.Append("\r\n\r\n");

                        var bodyBytes = new UTF8Encoding(false).GetBytes(sb.ToString());
                        var endBody = new UTF8Encoding(false).GetBytes("\r\n" + "--" + boundary + "--");

                        using (Stream bodyStream = request.GetRequestStream())
                        {
                            bodyStream.Write(bodyBytes, 0, bodyBytes.Length);
                            StreamHelper.CopyStream(body, bodyStream);
                            bodyStream.Write(endBody, 0, endBody.Length);
                        }
                    }
                    break;
                case RouteStyle.Download:
                    request.ContentType = null;
                    break;
                default:
                    throw new ArgumentNullException(nameof(routeStyle));
            }

            try
            {
                response = request.GetResponse() as HttpWebResponse;
                string result;

                if (routeStyle == RouteStyle.Download)
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (FileStream fs = System.IO.File.Create(requestArg))
                        {
                            StreamHelper.CopyStream(stream, fs);
                            result = "";
                        }
                    }
                }
                else
                {
                    result = ReadAsStringFromResponse(response);
                }

                response.Close();
                return result;
            }
            catch (WebException we)
            {
                response = we.Response as HttpWebResponse;
                if (response == null)
                {
                    throw ;
                }

                string responseStr = ReadAsStringFromResponse(response);
                string requestId;
                this.TryGetRequestId(responseStr, out requestId);

                if ((int)response.StatusCode >= 500)
                {
                    throw new RetryException(requestId, (int)response.StatusCode, responseStr, uri, we);
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    CheckForError(responseStr, uri);
                    throw new BadInputException(requestId, uri, responseStr, we);
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    if (RefreshToken())
                    {
                        throw new TokenRefreshedException();
                    }
                    throw new UnAuthorizedException(requestId, uri, responseStr, we);
                }
                else if ((int)response.StatusCode == RateLimitException.RateLimitStatusCode)
                {
                    int rateLimit = Convert.ToInt32(response.Headers.Get("X-Rate-Limit-Reset"));
                    throw new RateLimitException(requestId, rateLimit, uri, we);
                }
                else if (response.StatusCode == HttpStatusCode.PaymentRequired || 
                         response.StatusCode == HttpStatusCode.Forbidden ||
                         response.StatusCode == HttpStatusCode.Conflict)
                {
                    throw new BadResponseException(requestId, (int)response.StatusCode, uri, responseStr, we);
                }
                else
                {
                    throw new YfyHttpException(requestId, (int)response.StatusCode, uri, we.Message, we);
                }
            }
        }

        private bool TryGetRequestId(string response, out string requestId)
        {
            try
            {
                JObject jo = JObject.Parse(response);
                JToken token;
                if (jo.TryGetValue("request_id", out token))
                {
                    requestId = token.ToString();
                    return true;
                }
            }
            catch (JsonReaderException)
            {

            }
            requestId = "";
            return false;
        }

        private string ReadAsStringFromResponse(HttpWebResponse response)
        {
            return new StreamReader(response.GetResponseStream(), new UTF8Encoding(false))
                        .ReadToEnd();
        }

        private void CheckForError(string result, Uri uri)
        {
            try
            {
                JObject jo = JObject.Parse(result);
                JToken errors;
                if (jo.TryGetValue("errors", out errors))
                {
                    var yfyErrors = JsonConvert.DeserializeObject<YfyError>(result);

                    throw new YfyApiException(yfyErrors.RequestId, yfyErrors.Error[0].Code, yfyErrors.Error[0].Msg, uri);
                }
            }
            catch (JsonReaderException)
            {
                throw new YfyApiException("", HttpStatusCode.InternalServerError.ToString(), result, uri);
            }
        }

        private bool RefreshToken()
        {
            if (YfyClientConfig.Oauth2RefreshToken != "")
            {
                var authToken = OAuthHelper.RefreshOAuthToken(YfyClientConfig.Oauth2RefreshToken);
                YfyClientConfig.Oauth2AccessToken = authToken.AccessToken;
                YfyClientConfig.Oauth2RefreshToken = authToken.RefreshToken;

                if (YfyClientConfig.TokenRegister != null)
                {
                    YfyClientConfig.TokenRegister.RegisterAccessToken(authToken.AccessToken);
                }
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// 亿方云C#SDK客户端配置
    /// </summary>
    public sealed class YfyClientConfig
    {
        /// <summary>
        /// AccessToken
        /// </summary>
        public string Oauth2AccessToken { get; set; }

        /// <summary>
        /// RefreshToken
        /// </summary>
        public string Oauth2RefreshToken { get; set; } = "";

        /// <summary>
        /// SDK中Http请求的配置
        /// </summary>
        public YfyClientHttpConfig HttpConfig { get; set; } = new YfyClientHttpConfig();

        /// <summary>
        /// 在AccessToken刷新时的回调
        /// </summary>
        public IYfyTokenRegister TokenRegister { get; set; } = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="oauth2AccessToken">AccessToken</param>
        public YfyClientConfig(string oauth2AccessToken)
        {
            this.Oauth2AccessToken = oauth2AccessToken;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="oauth2AccessToken">AccessToken</param>
        /// <param name="oauth2RefreshToken">RefreshToken</param>
        public YfyClientConfig(string oauth2AccessToken, string oauth2RefreshToken)
            :this(oauth2AccessToken)
        {
            this.Oauth2RefreshToken = oauth2RefreshToken;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="oauth2AccessToken">AccessToken</param>
        /// <param name="httpConfig">SDK中Http请求的配置</param>
        public YfyClientConfig(string oauth2AccessToken, YfyClientHttpConfig httpConfig)
            :this(oauth2AccessToken)
        {
            this.HttpConfig = httpConfig;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="oauth2AccessToken">AccessToken</param>
        /// <param name="oauth2RefreshToken">RefreshToken</param>
        /// <param name="httpConfig">SDK中Http请求的配置</param>
        public YfyClientConfig(string oauth2AccessToken, string oauth2RefreshToken, YfyClientHttpConfig httpConfig)
            : this(oauth2AccessToken, oauth2RefreshToken)
        {
            this.HttpConfig = httpConfig;
        }
    }

    /// <summary>
    /// SDK中Http请求的配置
    /// </summary>
    public class YfyClientHttpConfig
    {
        /// <summary>
        /// HttpWebRequest TimeOut
        /// </summary>
        public int Timeout { get; set; } = 100000;

        /// <summary>
        /// 最大重试次数
        /// </summary>
        public int MaxRetries { get; set; } = 5;

        /// <summary>
        /// 代理配置
        /// </summary>
        public WebProxy Proxy { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public YfyClientHttpConfig()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="timeout">time out时间</param>
        /// <param name="maxRetries">最大重试次数</param>
        /// <param name="proxy">代理配置</param>
        public YfyClientHttpConfig(int timeout, int maxRetries, WebProxy proxy)
        {
            this.Timeout = timeout;
            this.MaxRetries = maxRetries;
            this.Proxy = proxy;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="proxy">代理配置</param>
        public YfyClientHttpConfig(WebProxy proxy)
        {
            this.Proxy = proxy;
        }
    }

    internal static class HttpMethod
    {
        public const string Post = "POST";
        public const string Get = "GET";
    }
}

namespace Yfy.Api.Exceptions
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;

    /// <summary>
    /// 由http请求引发的异常，是api异常的基类，同时还包括网络异常。
    /// </summary>
    public class YfyHttpException : YfyException
    {
        /// <summary>
        /// Http Status Code
        /// </summary>
        public int StatusCode { get; private set; }

        /// <summary>
        /// 引发异常的请求uri
        /// </summary>
        public Uri RequestUri { get; private set; }

        internal YfyHttpException(string requestId, int statusCode, Uri requestUri = null, string message = "", System.Exception inner = null)
            :base(requestId, message, inner)
        {
            this.StatusCode = statusCode;
            this.RequestUri = requestUri;
        }

        internal YfyHttpException(WebException we)
            :base("", new StreamReader(we.Response.GetResponseStream(), new UTF8Encoding(false))
                        .ReadToEnd(), we)
        {
        }

        /// <summary>
        /// 返回此异常的通用错误信息
        /// </summary>
        /// <returns>
        /// 通用错误信息的字符串
        /// </returns>
        public override string ToString()
        {
            return
                $"YfyHttpException! RequestId = {this.RequestId}, StatusCode = {this.StatusCode}, RequestUri = {this.RequestUri}, Message = {this.Message}";
        }
    }
}

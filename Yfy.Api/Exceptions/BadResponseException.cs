namespace Yfy.Api.Exceptions
{
    using System;

    /// <summary>
    /// Http 402, 403, 409异常
    /// </summary>
    public class BadResponseException : YfyHttpException
    {
        internal BadResponseException(string requestId, int statusCode, Uri requestUri = null, string message = null, System.Exception inner = null)
            :base(requestId, statusCode, requestUri, message, inner)
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
                $"BadResponseException! RequestId = {this.RequestId}, RequestUri = {this.RequestUri}, Message = {this.Message}";
        }
    }
}

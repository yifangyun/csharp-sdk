namespace Yfy.Api.Exceptions
{
    using System;

    /// <summary>
    /// 服务器异常
    /// </summary>
    public class InternalServerException : YfyHttpException
    {
        internal InternalServerException(string requestId, int statusCode, Uri requestUri = null, string message = "", System.Exception inner = null)
            : base(requestId, statusCode, requestUri, message, inner)
        {
        }

        internal InternalServerException(RetryException re)
            :base(re.RequestId, re.StatusCode, re.RequestUri, re.Message, re.InnerException)
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
                $"InternalServerException! RequestId = {this.RequestId}, StatusCode {this.StatusCode}, RequestUri = {this.RequestUri}, Message = {this.Message}";
        }
    }
}

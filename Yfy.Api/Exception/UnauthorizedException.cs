namespace Yfy.Api.Exception
{
    using System;

    /// <summary>
    /// 认证失败
    /// </summary>
    public class UnAuthorizedException : YfyHttpException
    {
        internal UnAuthorizedException(string requestId, Uri requestUri = null, string message = null, System.Exception inner = null)
            :base(requestId, (int)System.Net.HttpStatusCode.Unauthorized, requestUri, message, inner)
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
                $"UnAuthorizedException! RequestId = {this.RequestId}, RequestUri = {this.RequestUri}, Message = {this.Message}.";
        }
    }
}

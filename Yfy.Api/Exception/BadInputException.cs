namespace Yfy.Api.Exception
{
    using System;

    /// <summary>
    /// 请求参数不合法
    /// </summary>
    public class BadInputException : YfyHttpException
    {
        internal BadInputException(string requestId, Uri requestUri = null, string message = "", System.Exception inner = null)
            :base(requestId, (int)System.Net.HttpStatusCode.BadRequest, requestUri, message, inner)
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
                $"BadInputException! RequestId = {this.RequestId}, RequestUri = {this.RequestUri}, Message = {this.Message}";
        }
    }
}

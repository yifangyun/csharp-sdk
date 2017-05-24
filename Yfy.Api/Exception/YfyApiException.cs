namespace Yfy.Api.Exception
{
    using System;

    /// <summary>
    /// 所有亿方云api异常的基类
    /// </summary>
    public class YfyApiException : YfyHttpException
    {
        /// <summary>
        /// 对应错误中的errors code 字段
        /// </summary>
        public string ErrorCode { get; private set; }

        /// <summary>
        /// 对应错误中的errors msg 字段
        /// </summary>
        public string ErrorMsg { get; private set; }

        internal YfyApiException(string requestId, string errorCode, string errorMsg, Uri requestUri, System.Exception inner = null)
            : base(requestId, (int)System.Net.HttpStatusCode.BadRequest, requestUri, errorMsg, inner)
        {
            this.ErrorCode = errorCode;
            this.ErrorMsg = errorMsg;
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
                $"YfyApiException! RequestId = {this.RequestId}, ErrorCode = {this.ErrorCode}, ErrorMsg = {this.ErrorMsg}, RequestUri = {this.RequestUri}";
        }
    }
}

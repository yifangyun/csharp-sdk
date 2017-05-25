namespace Yfy.Api.Exceptions
{
    using System;

    /// <summary>
    /// api调用次数达到上限
    /// </summary>
    public class RateLimitException : YfyHttpException
    {
        /// <summary>
        /// 引发该异常之后，在该时间（秒）之后重试
        /// </summary>
        public int RetryAfterTime { get; private set; }

        /// <summary>
        /// 接口调用限制Http状态码常量
        /// </summary>
        public const int RateLimitStatusCode = 429;

        internal RateLimitException(string requestId, int rateLimit, Uri requestUri = null, System.Exception inner = null)
            :base(requestId, RateLimitStatusCode, requestUri, BuildRateLimitExceptionMsg(rateLimit),  inner)
        {
            this.RetryAfterTime = rateLimit;
        }

        private static string BuildRateLimitExceptionMsg(int rateLimit)
        {
            return $"Retry after {rateLimit} second(s)";
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
                $"RateLimitException! RequestId = {this.RequestId}, RequestUri = {this.RequestUri}, Message = {this.Message}";
        }
    }
}

namespace Yfy.Api.Exceptions
{
    using System;

    /// <summary>
    /// 由http 500引发的重试异常
    /// </summary>
    internal class RetryException : YfyHttpException
    {
        internal RetryException(string requestId, int statusCode, string message = null, Uri requestUri = null, System.Exception inner = null)
            :base(requestId, statusCode, requestUri, message,  inner)
        {
        }
    }
}

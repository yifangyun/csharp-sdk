namespace Yfy.Api.Exception
{
    using System.Text;

    /// <summary>
    /// 亿方云api中所有关于亿方云相关的异常的基类
    /// </summary>
    public class YfyException : System.Exception
    {
        /// <summary>
        /// 引发关于亿方云异常时的requestId
        /// </summary>
        public string RequestId { get; private set; }

        internal YfyException(string requestId, string message = "", System.Exception inner = null)
            : base(message, inner)
        {
            this.RequestId = requestId;
        }
        /// <summary>
        /// 返回此异常的通用错误信息
        /// </summary>
        /// <returns>
        /// 通用错误信息的字符串
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(base.ToString());

            if (this.RequestId != null)
            {
                sb.AppendFormat("YfyException! RequestId = {0}, Message = {1}", this.RequestId, this.Message);
            }

            return sb.ToString();
        }
    }
}

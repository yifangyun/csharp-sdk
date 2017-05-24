namespace Yfy.Api.Exception
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// 发生api异常时的数据返回
    /// </summary>
    internal class YfyError
    {
        [JsonProperty("errors")]
        public List<ErrorContent> Error { get; private set; }

        [JsonProperty("request_id")]
        public string RequestId { get; private set; }
    }

    /// <summary>
    /// 单个error的结构体
    /// </summary>
    internal class ErrorContent
    {
        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }
}

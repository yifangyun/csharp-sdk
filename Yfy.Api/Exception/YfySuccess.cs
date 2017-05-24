namespace Yfy.Api.Exception
{
    using Newtonsoft.Json;

    /// <summary>
    /// 请求成功时的返回
    /// </summary>
    internal class YfySuccess
    {
        [JsonProperty("success")]
        public bool Success { get; private set; }
    }
}

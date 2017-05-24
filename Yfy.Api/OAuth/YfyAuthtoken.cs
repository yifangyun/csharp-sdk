namespace Yfy.Api.Oauth
{
    using Newtonsoft.Json;

    /// <summary>
    /// 通用亿方云AuthToken对象
    /// </summary>
    public class YfyAuthtoken : YfyObject
    {
        /// <summary>
        /// access_token
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// token_type, 目前固定为bearer
        /// </summary>
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        /// <summary>
        /// refresh_token
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// 授权范围，目前固定为"all"
        /// </summary>
        [JsonProperty("scope")]
        public string Scope { get; set; }
    }
}

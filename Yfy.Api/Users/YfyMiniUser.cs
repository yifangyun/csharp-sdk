namespace Yfy.Api.Users
{
    using Newtonsoft.Json;

    /// <summary>
    /// mini用户
    /// </summary>
    public class YfyMiniUser : YfyObject
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// 用户名字
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 用户登录方式
        /// </summary>
        [JsonProperty("login")]
        public string Login { get; set; }

        /// <summary>
        /// 用户企业id
        /// </summary>
        [JsonProperty("enterprise_id")]
        public long EnterpriseId { get; set; }
    }
}

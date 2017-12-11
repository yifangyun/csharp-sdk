namespace Yfy.Api.Users
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using Yfy.Api.Enterprises;

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
        [JsonProperty("enterprise")]
        public YfyMiniEnterprise Enterprise { get; set; }
    }

    /// <summary>
    /// mini user collection
    /// </summary>
    public class YfyMiniUserCollection
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        [JsonProperty("users")]
        public List<YfyMiniUser> users { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        [JsonProperty("page_id")]
        public int pageId { get; set; }

        /// <summary>
        /// 总用户数
        /// </summary>
        [JsonProperty("total_count")]
        public long totalCount { get; set; }

        /// <summary>
        /// 页容量
        /// </summary>
        [JsonProperty("page_capacity")]
        public int pageCapacity { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        [JsonProperty("page_count")]
        public int pageCount { get; set; }
    }
}

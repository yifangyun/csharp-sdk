namespace Yfy.Api.Enterprises
{
    using Newtonsoft.Json;

    /// <summary>
    /// 通用亿方云简单企业
    /// </summary>
    public class YfyMiniEnterprise : YfyObject
    {
        /// <summary>
        /// 企业id
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// 企业名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 企业管理员id
        /// </summary>
        [JsonProperty("admin_user_id")]
        public long AdminUserId { get; set; }
    }
}

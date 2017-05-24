namespace Yfy.Api.ShareLink
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.ComponentModel;
    using System.Collections.Generic;

    /// <summary>
    /// 通用分享链接对象
    /// </summary>
    public class YfyShareLink : YfyObject
    {
        /// <summary>
        /// 分享标识符
        /// </summary>
        [JsonProperty("unique_name")]
        public string UniqueName { get; set; }

        /// <summary>
        /// 完整分享链接
        /// </summary>
        [JsonProperty("share_link")]
        public string ShareLinkUrl { get; set; }

        /// <summary>
        /// 分享链接权限（公开和企业）
        /// </summary>
        [JsonProperty("access")]
        [JsonConverter(typeof(LowerStringEnumConverter))]
        [DefaultValue(ShareLinkAccess.Public)]
        public ShareLinkAccess Access { get; set; }

        /// <summary>
        /// 是否有密码
        /// </summary>
        [JsonProperty("password_protected", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool PasswordProtected { get; set; }

        /// <summary>
        /// 过期时间（yyyy-MM-dd）
        /// </summary>
        [JsonProperty("due_time")]
        [JsonConverter(typeof(ChinaDateTimeConverter))]
        public DateTime DueTime { get; set; }

        /// <summary>
        /// 是否禁止下载
        /// </summary>
        [JsonProperty("disable_download", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool DisableDownload { get; set; }
    }

    /// <summary>
    /// 通用分享链接集合
    /// </summary>
    public class YfyShareLinkCollection
    {
        /// <summary>
        /// 分享链接
        /// </summary>
        [JsonProperty("share_links")]
        public List<YfyShareLink> ShareLinks { get; set; }

        /// <summary>
        /// 分享链接总数
        /// </summary>
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        /// <summary>
        /// 分页索引
        /// </summary>
        [JsonProperty("page_id")]
        public int PageId { get; set; }

        /// <summary>
        /// 分页容量
        /// </summary>
        [JsonProperty("page_capacity")]
        public int PageCapacity { get; set; }

        /// <summary>
        /// 分页总页数
        /// </summary>
        [JsonProperty("page_count")]
        public int PageCount { get; set; }
    }

    /// <summary>
    /// 分享链接访问权限
    /// </summary>
    public enum ShareLinkAccess
    {
        /// <summary>
        /// 公开
        /// </summary>
        Public,

        /// <summary>
        /// 公司内访问
        /// </summary>
        Company,
    }

    internal class ChinaDateTimeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(DateTime));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            DateTime date = (DateTime)value;
            writer.WriteValue(string.Format("{0:yyyy-MM-dd}", date));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = reader.Value.ToString();
            DateTime? date = value == "" ? (DateTime?)null : DateTime.Parse(value);
            return date;
        }
    }

    internal class LowerStringEnumConverter : JsonConverter
    {
        private static StringEnumConverter sec = new StringEnumConverter();

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(ShareLinkAccess));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var str = value.ToString().ToLower();
            writer.WriteValue(str);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return sec.ReadJson(reader, objectType, existingValue, serializer);
        }
    }
}

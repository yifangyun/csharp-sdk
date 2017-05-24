namespace Yfy.Api.Folders
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Yfy.Api.Items;

    /// <summary>
    /// 路径文件夹
    /// </summary>
    public class YfyPathFolder : YfyObject
    {
        /// <summary>
        /// 文件夹id
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// 文件夹类型，目前固定为folder
        /// </summary>
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ItemType Type { get; set; }

        /// <summary>
        /// 文件夹名字
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}

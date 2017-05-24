namespace Yfy.Api.Items
{
    using System.Collections.Generic;
    using Yfy.Api.Users;
    using Yfy.Api.Folders;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Yfy.Api.Files;

    /// <summary>
    /// 文件（文件夹）的基类
    /// </summary>
    public class YfyItem : YfyObject
    {
        /// <summary>
        /// 文件（文件夹）id
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// 文件（文件夹）类型
        /// </summary>
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ItemType Type { get; set; }

        /// <summary>
        /// 文件（文件夹）名字
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 文件（文件夹）大小
        /// </summary>
        [JsonProperty("size")]
        public long Size { get; set; }

        /// <summary>
        /// 文件（文件夹）创建时间
        /// </summary>
        [JsonProperty("created_at")]
        public int CreatedAt { get; set; }

        /// <summary>
        /// 文件（文件夹）最后修改时间
        /// </summary>
        [JsonProperty("modified_at")]
        public int ModifiedAt { get; set; }

        /// <summary>
        /// 文件（文件夹）描述
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// 文件（文件夹）所有者
        /// </summary>
        [JsonProperty("owned_by")]
        public YfyMiniUser OwnedBy { get; set; }

        /// <summary>
        /// 文件（文件夹）父文件夹
        /// </summary>
        [JsonProperty("parent")]
        public YfyPathFolder Parent { get; set; }

        /// <summary>
        /// 文件夹路径
        /// </summary>
        [JsonProperty("path")]
        public List<YfyPathFolder> Path { get; set; }

        /// <summary>
        /// 文件（文件夹）序列id，目前没有用
        /// </summary>
        [JsonProperty("sequence_id")]
        public long SequenceId { get; set; }
    }

    /// <summary>
    /// 文件（文件夹）集合
    /// </summary>
    public class YfyItemCollection : YfyObject
    {
        /// <summary>
        /// 集合中的文件
        /// </summary>
        [JsonProperty("files")]
        public List<YfyFile> Files { get; set; }

        /// <summary>
        /// 集合中的文件夹
        /// </summary>
        [JsonProperty("folders")]
        public List<YfyFolder> Folders { get; set; }

        /// <summary>
        /// 集合中的文件（文件夹）的总数量
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
    }

    /// <summary>
    /// 文件（文件夹）类型
    /// </summary>
    public enum ItemType
    {
        /// <summary>
        /// 文件
        /// </summary>
        file,

        /// <summary>
        /// 文件夹
        /// </summary>
        folder,

        /// <summary>
        /// 所有，即文件和文件夹
        /// </summary>
        all,
    }
}
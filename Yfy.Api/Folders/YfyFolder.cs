namespace Yfy.Api.Folders
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using Yfy.Api.Items;

    /// <summary>
    /// 通用文件夹对象
    /// </summary>
    public class YfyFolder : YfyItem
    {
        /// <summary>
        /// 文件夹下文件（和文件夹）的数量
        /// </summary>
        [JsonProperty("item_count")]
        public long ItemCount { get; set; }

        /// <summary>
        /// 文件夹类型
        /// </summary>
        [JsonProperty("folder_type")]
        public string FolderType { get; set; }
    }
}

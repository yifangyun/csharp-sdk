namespace Yfy.Api.Comment
{
    using Yfy.Api.Users;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    
    /// <summary>
    /// 通用评论对象
    /// </summary>
    public class YfyComment : YfyObject
    {
        /// <summary>
        /// 评论id
        /// </summary>
        [JsonProperty("comment_id")]
        public long Id { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }

        /// <summary>
        /// 评论创建时间
        /// </summary>
        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        /// <summary>
        /// 评论文件id
        /// </summary>
        [JsonProperty("file_id")]
        public long FileId { get; set; }

        /// <summary>
        /// 评论用户
        /// </summary>
        [JsonProperty("user")]
        public YfyMiniUser User { get; set; }
    }

    /// <summary>
    /// 评论集合
    /// </summary>
    public class YfyCommentCollection
    {
        /// <summary>
        /// 评论集合
        /// </summary>
        [JsonProperty("comments")]
        public List<YfyComment> Comments { get; set; }
    }
}

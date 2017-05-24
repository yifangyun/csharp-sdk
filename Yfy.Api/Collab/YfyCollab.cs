namespace Yfy.Api.Collab
{
    using Yfy.Api.Users;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Collections.Generic;

    /// <summary>
    /// 通用协作对象
    /// </summary>
    public class YfyCollab : YfyObject
    {
        /// <summary>
        /// 协作id
        /// </summary>
        [JsonProperty("collab_id")]
        public long Id { get; set; }

        /// <summary>
        /// 协作用户
        /// </summary>
        [JsonProperty("user")]
        public YfyMiniUser User { get; set; }

        /// <summary>
        /// 是否接受协作
        /// </summary>
        [JsonProperty("accepted")]
        public bool IsAccepted { get; set; }

        /// <summary>
        /// 协作角色
        /// </summary>
        [JsonProperty("role")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CollabRole Role { get; set; }
    }

    /// <summary>
    /// 协作集合
    /// </summary>
    public class YfyCollabCollection
    {
        /// <summary>
        /// 用户最终的协作角色
        /// </summary>
        [JsonProperty("final_role")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CollabRole FinalRole { get; set; }

        /// <summary>
        /// 用户参与的协作信息集合
        /// </summary>
        [JsonProperty("collab_info")]
        public List<YfyCollab> Collabs { get; set; }
    }

    /// <summary>
    /// 协作角色
    /// </summary>
    public enum CollabRole
    {
        /// <summary>
        /// 上传者
        /// </summary>
        uploader,

        /// <summary>
        /// 预览者
        /// </summary>
        previewer,

        /// <summary>
        /// 预览上传者
        /// </summary>
        previewer_uploader,

        /// <summary>
        /// 查看者
        /// </summary>
        viewer,

        /// <summary>
        /// 查看上传者
        /// </summary>
        viewer_uploader,

        /// <summary>
        /// 编辑者
        /// </summary>
        editor,

        /// <summary>
        /// 共同所有者
        /// </summary>
        coowner,

        /// <summary>
        /// 所有者
        /// </summary>
        owner,
    }
}

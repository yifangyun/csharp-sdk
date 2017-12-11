namespace Yfy.Api.Department
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;

    using Yfy.Api.Users;

    /// <summary>
    /// yfy department object
    /// </summary>
    public class YfyDepartment : YfyObject
    {
        /// <summary>
        /// 部门id
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// 部门创建时间
        /// </summary>
        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 部门管理者（可能为null）
        /// </summary>
        [JsonProperty("director")]
        public YfyMiniUser Director { get; set; }

        /// <summary>
        /// 部门的父部门id
        /// </summary>
        [JsonProperty("parent_id")]
        public long ParentId { get; set; }

        /// <summary>
        /// 部门成员数
        /// </summary>
        [JsonProperty("user_count")]
        public long UserCount { get; set; }

        /// <summary>
        /// 部门的子部门个数
        /// </summary>
        [JsonProperty("children_departments_count")]
        public long ChildrenDepartmentsCount { get; set; }

        /// <summary>
        /// 部门的item个数
        /// </summary>
        [JsonProperty("direct_item_count")]
        public long DirectItemCount { get; set; }
    }
}

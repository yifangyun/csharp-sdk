namespace Yfy.Api.Department
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;

    /// <summary>
    /// yfy mini department
    /// </summary>
    public class YfyMiniDepartment : YfyObject
    {
        /// <summary>
        /// 部门id
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

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

    /// <summary>
    /// yfy mini department collection
    /// </summary>
    public class YfyMiniDepartmentCollection
    {
        /// <summary>
        /// 获取部门的子部门列表
        /// </summary>
        [JsonProperty("children")]
        public List<YfyMiniDepartment> Children { get; set; }
    }
}

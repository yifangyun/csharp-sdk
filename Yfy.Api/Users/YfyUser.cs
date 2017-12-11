namespace Yfy.Api.Users
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using Yfy.Api.Enterprises;

    /// <summary>
    /// 通用用户对象
    /// </summary>
    public class YfyUser : YfyObject
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// 企业id
        /// </summary>
        [JsonProperty("enterprise")]
        public YfyMiniEnterprise Enterprise { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// 用户登录名（邮箱或手机）
        /// </summary>
        [JsonProperty("login")]
        public string Login { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// 用户头像下载所需的key
        /// </summary>
        [JsonProperty("profile_pic_key")]
        public string ProfilePickey { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        [JsonProperty("active")]
        public bool IsActive { get; set; }

        /// <summary>
        /// 用户姓名的拼音字母
        /// </summary>
        [JsonProperty("full_name_pinyin")]
        public string FullNamePinyin { get; set; }

        /// <summary>
        /// 用户姓名的拼音首字母
        /// </summary>
        [JsonProperty("pinyin_first_letters")]
        public string PinyinFirstLetters { get; set; }
    }

    /// <summary>
    /// 通用用户集合
    /// </summary>
    public class YfyUserCollection
    {
        /// <summary>
        /// 用户
        /// </summary>
        [JsonProperty("users")]
        public List<YfyUser> Users { get; set; }

        /// <summary>
        /// 总用户数
        /// </summary>
        [JsonProperty("total_count")]
        public long TotalCount { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        [JsonProperty("page_id")]
        public int PageId { get; set; }

        /// <summary>
        /// 页容量
        /// </summary>
        [JsonProperty("page_capacity")]
        public int PageCapacity { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        [JsonProperty("page_count")]
        public int PageCount { get; set; }
    }
}

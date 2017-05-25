namespace Yfy.Api.Files
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.ComponentModel;

    internal class UploadFileArg
    {
        [JsonProperty("parent_id")]
        public long ParentId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("upload_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        [DefaultValue(UploadType.api)]
        public UploadType UploadType { get; set; }

        public UploadFileArg(long parentId, string name, UploadType uploadType = UploadType.api)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name should not be null or empty", nameof(name));
            }

            this.ParentId = parentId;
            this.UploadType = uploadType;
            this.Name = name;
        }
    }

    /// <summary>
    /// 上传类型，目前固定为api
    /// </summary>
    public enum UploadType
    {
        /// <summary>
        /// api
        /// </summary>
        api,
    }

    internal class UploadFileNewVersionArg
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("remark")]
        public string Remark { get; set; }

        [JsonProperty("upload_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        [DefaultValue(UploadType.api)]
        public UploadType UploadType { get; set; }

        public UploadFileNewVersionArg(string name, string remark = null, UploadType uploadType = UploadType.api)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name should not be null or empty", nameof(name));
            }

            this.Name = name;
            this.Remark = remark;
            this.UploadType = uploadType;
        }
    }

    internal class PreSignatureUploadUrl
    {
        [JsonProperty("presign_url")]
        public string PresignUrl { get; private set; }
    }
}

namespace Yfy.Api.ShareLink
{
    using System;
    using Newtonsoft.Json;
    using System.ComponentModel;

    internal class UpdateShareLinkArg
    {
        [JsonProperty("access")]
        [JsonConverter(typeof(LowerStringEnumConverter))]
        public ShareLinkAccess Access { get; set; }

        [JsonProperty("disable_download", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool DisableDownload { get; set; }

        [JsonProperty("due_time")]
        [JsonConverter(typeof(ChinaDateTimeConverter))]
        public DateTime DueTime { get; set; }

        [JsonProperty("password_protected", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool PasswordProtected { get; set; }

        [JsonProperty("password")]
        [DefaultValue(null)]
        public string Password { get; set; }

        public UpdateShareLinkArg(ShareLinkAccess access, DateTime dueTime, bool disableDownload = false, bool passwordProtected = false, string password = null)
        {
            this.Access = access;
            this.DueTime = dueTime;
            this.DisableDownload = disableDownload;
            this.PasswordProtected = passwordProtected;
            this.Password = password;
        }
    }
}

namespace Yfy.Api.Files
{
    using Newtonsoft.Json;

    internal class DownloadFileUrl
    {
        [JsonProperty("download_url")]
        public string DownloadUrl { get; set; }
    }
}

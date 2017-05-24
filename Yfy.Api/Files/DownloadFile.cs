namespace Yfy.Api.Files
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    internal class DownloadFileUrl
    {
        [JsonProperty("download_urls")]
        public Dictionary<long, string> DownloadUrls { get; set; }
    }
}

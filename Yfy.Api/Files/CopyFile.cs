namespace Yfy.Api.Files
{
    using Newtonsoft.Json;

    internal class CopyFileArg
    {
        [JsonProperty("target_folder_id")]
        public long TargetFolderId { get; set; }

        public CopyFileArg(long targetFolderId)
        {
            this.TargetFolderId = targetFolderId;
        }
    }
}

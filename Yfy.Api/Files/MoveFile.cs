namespace Yfy.Api.Files
{
    using Newtonsoft.Json;

    internal class MoveFileArg
    {
        [JsonProperty("target_folder_id")]
        public long TargetFolderId { get; set; }

        public MoveFileArg(long targetFolderId)
        {
            this.TargetFolderId = targetFolderId;
        }
    }
}

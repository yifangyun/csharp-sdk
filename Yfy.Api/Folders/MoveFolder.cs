namespace Yfy.Api.Folders
{
    using Newtonsoft.Json;

    internal class MoveFolderArg
    {
        [JsonProperty("target_folder_id")]
        public long TargetFolderId { get; set; }

        public MoveFolderArg(long targetFolderId)
        {
            this.TargetFolderId = targetFolderId;
        }
    }
}

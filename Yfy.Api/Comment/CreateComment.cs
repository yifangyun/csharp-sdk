namespace Yfy.Api.Comment
{
    using Newtonsoft.Json;

    internal class CreateCommentArg
    {
        [JsonProperty("file_id")]
        public long FileId { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        public CreateCommentArg(long fileId, string content)
        {
            this.FileId = fileId;
            this.Content = content;
        }
    }
}

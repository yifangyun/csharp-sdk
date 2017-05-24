namespace Yfy.Api.Folders
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.ComponentModel;
    using Yfy.Api.Items;

    internal class GetDirectChildrenArg
    {
        [JsonProperty("folder_id")]
        public long FolderId { get; set; }

        [JsonProperty("page_id")]
        [DefaultValue(0)]
        public int PageId { get; set; }

        [JsonProperty("page_capacity")]
        [DefaultValue(20)]
        public int PageCapacity { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        [DefaultValue(ItemType.all)]
        public ItemType Type { get; set; }

        public GetDirectChildrenArg(long folderId, int pageId = 0, int pageCapacity = 20, ItemType type = ItemType.all)
        {
            this.FolderId = folderId;
            this.PageId = pageId;
            this.PageCapacity = pageCapacity;
            this.Type = type;
        }
    }
}

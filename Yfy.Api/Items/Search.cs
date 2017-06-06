namespace Yfy.Api.Items
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.ComponentModel;

    internal class SearchArg
    {
        [JsonProperty("query_words")]
        public string QueryWords { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        [DefaultValue(ItemType.all)]
        public ItemType Type { get; set; }

        [JsonProperty("page_id")]
        [DefaultValue(0)]
        public int PageId { get; set; }

        [JsonProperty("search_in_folder")]
        [DefaultValue(0)]
        public long SearchInFolder { get; set; }

        public SearchArg(string queryWords, long searchInFolder = 0, ItemType type = ItemType.all, int pageNumber = 0)
        {
            this.QueryWords = queryWords;
            this.SearchInFolder = searchInFolder;
            this.Type = type;
            this.PageId = pageNumber;
        }
    }
}

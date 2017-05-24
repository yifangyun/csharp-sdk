namespace Yfy.Api.Trash
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Yfy.Api.Items;

    internal class ClearTrashArg
    {
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ItemType Type { get; set; }

        public ClearTrashArg(ItemType type)
        {
            this.Type = type;
        }
    }
}

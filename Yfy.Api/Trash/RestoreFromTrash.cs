namespace Yfy.Api.Trash
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Yfy.Api.Items;

    internal class RestoreAllFromTrashArg
    {
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ItemType Type { get; set; }

        public RestoreAllFromTrashArg(ItemType type)
        {
            this.Type = type;
        }
    }
}

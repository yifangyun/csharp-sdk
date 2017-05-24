namespace Yfy.Api.Collab
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    internal class UpdateCollabArg
    {
        [JsonProperty("role")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CollabRole Role { get; set; }

        public UpdateCollabArg(CollabRole role)
        {
            this.Role = role;
        }
    }
}

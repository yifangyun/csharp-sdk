namespace Yfy.Api.Files
{
    using Newtonsoft.Json;
    using System.ComponentModel;

    internal class UpdateFileArg
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        [DefaultValue(null)]
        public string Description { get; set; }

        public UpdateFileArg(string name, string description = null)
        { 
            this.Name = name;
            this.Description = description;
        }
    }
}

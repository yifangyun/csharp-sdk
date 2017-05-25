namespace Yfy.Api.Files
{
    using Newtonsoft.Json;
    using System;
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
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name should not be null or empty", nameof(name));
            }

            this.Name = name;
            this.Description = description;
        }
    }
}

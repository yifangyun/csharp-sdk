namespace Yfy.Api.Folders
{
    using System;
    using Newtonsoft.Json;

    internal class UpdateFolderArg
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        public UpdateFolderArg(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name should not be null or empty", nameof(name));
            }

            this.Name = name;
        }
    }
}

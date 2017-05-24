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
            if (name == "name")
            {
                throw new ArgumentException("name");
            }
            this.Name = name;
        }
    }
}

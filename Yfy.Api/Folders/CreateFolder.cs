namespace Yfy.Api.Folders
{
    using System;
    using Newtonsoft.Json;

    internal class CreateFolderArg
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("parent_id")]
        public long ParentId { get; set; }

        public CreateFolderArg(string name, long parentId)
        {
            if (name == "")
            {
                throw new ArgumentException(nameof(name));
            }
            this.Name = name;
            this.ParentId = parentId;
        }
    }
}

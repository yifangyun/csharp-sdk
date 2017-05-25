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
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name should not be null or empty", nameof(name));
            }

            this.Name = name;
            this.ParentId = parentId;
        }
    }
}

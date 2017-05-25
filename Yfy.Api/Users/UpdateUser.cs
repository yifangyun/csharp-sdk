namespace Yfy.Api.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;

    internal class UpdateUserArg
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        public UpdateUserArg(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name should not be null or empty", nameof(name));
            }

            this.Name = name;
        }
    }
}

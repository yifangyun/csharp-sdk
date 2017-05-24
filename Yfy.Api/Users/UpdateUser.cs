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
            this.Name = name;
        }
    }
}

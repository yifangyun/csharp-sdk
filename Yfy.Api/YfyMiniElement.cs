using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yfy.Api
{
    public class YfyMiniElement : YfyObject
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }
    }
}

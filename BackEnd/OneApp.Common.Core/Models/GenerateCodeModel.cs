using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OneApp.Common.Core.Models
{
    public class GenerateCodeModel
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("key_initial_value")]
        public string KeyInitialValue { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }
}

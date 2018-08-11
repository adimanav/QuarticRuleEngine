using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace QuarticRuleEngine.InputFileParser
{
    public class Data
    {
        [JsonProperty(PropertyName = "signal")]
        public string Signal { get; set; }

        [JsonProperty(PropertyName = "value_type")]
        public string ValueType { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QuarticRuleEngine.InputFileParser
{
    internal class InputFileParser
    {
        public static List<Data> Parse(string filePath)
        {
            return JsonConvert.DeserializeObject<List<Data>>(File.ReadAllText(filePath));
        }
    }
}

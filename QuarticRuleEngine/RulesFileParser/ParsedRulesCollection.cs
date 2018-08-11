using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace QuarticRuleEngine.RulesFileParser
{
    [Serializable()]
    [XmlRoot("Root")]
    public class ParsedRulesCollection
    {
        [XmlArray("RulesCollection")]
        [XmlArrayItem("Rule", typeof(ParsedRule))]
        public ParsedRule[] Rules { get; set; }
    }
}

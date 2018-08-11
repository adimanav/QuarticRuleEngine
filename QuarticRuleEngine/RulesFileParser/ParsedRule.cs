using System;
using System.Collections.Generic;
using System.Text;

namespace QuarticRuleEngine.RulesFileParser
{
    [Serializable()]
    public class ParsedRule
    {
        [System.Xml.Serialization.XmlElement("Signal")]
        public string Signal { get; set; }

        [System.Xml.Serialization.XmlElement("Operator")]
        public string Operator { get; set; }

        [System.Xml.Serialization.XmlElement("Value")]
        public string Value { get; set; }

        [System.Xml.Serialization.XmlElement("Type")]
        public string Type { get; set; }

    }
}

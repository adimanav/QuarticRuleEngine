using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace QuarticRuleEngine.RulesFileParser
{
    internal class RulesFileParser
    {
        private static void ValidateXmlFile(string xmlFilePath)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(assembly.GetManifestResourceNames()[0]))
            {
                XmlReaderSettings settings = new XmlReaderSettings();

                XmlSchema schema = XmlSchema.Read(stream, OnXsdSyntaxError);
                settings.ValidationType = ValidationType.Schema;
                settings.Schemas.Add(schema);
                settings.ValidationEventHandler += OnXmlSyntaxError;

                using (XmlReader validator = XmlReader.Create(xmlFilePath, settings))
                {
                    // Validate the entire xml file
                    while (validator.Read()) ;
                }
            }
        }

        private static void OnXmlSyntaxError(object sender, ValidationEventArgs e)
        {
            throw new InvalidDataException();
        }

        private static void OnXsdSyntaxError(object sender, ValidationEventArgs e)
        {
            throw new InvalidDataException();
        }

        public static ParsedRulesCollection Parse(string filePath)
        {
            ValidateXmlFile(filePath);

            XmlSerializer serializer = new XmlSerializer(typeof(ParsedRulesCollection));

            StreamReader reader = new StreamReader(filePath);
            var rules = (ParsedRulesCollection)serializer.Deserialize(reader);
            reader.Close();

            return rules;
        }
    }
}

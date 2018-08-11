using System;
using System.Collections.Generic;
using QuarticRuleEngine.InputFileParser;
using QuarticRuleEngine.RulesFileParser;
using QuarticRuleEngine.RulesManager;

namespace QuarticRuleEngine
{
    public class RulesEngine
    {
        public static readonly Type[] SupportedTypes =
        {
            typeof(int),
            typeof(string),
            typeof(DateTime)
        };


        private object[] TypewiseRuleCollection
        {
            get;
            set;
        }

        public RulesEngine(string filePath)
        {
            ReloadFromFile(filePath);
        }


        public List<Data> Run(string filePath)
        {
            var result = new List<Data>();

            var inputData = InputFileParser.InputFileParser.Parse(filePath);
            foreach (var data in inputData)
            {
                switch (data.ValueType)
                {
                    case "Integer":
                    case "integer":
                        {
                            var manager = TypewiseRuleCollection[0] as RulesManager<int>;
                            var rules = manager.GetRules(data.Signal);
                            if (null != rules)
                            {
                                decimal parsedValue = decimal.Parse(data.Value);
                                int roundedValue = (int)Math.Round(parsedValue);
                                var mgrData = new Data<int>(data.Signal, roundedValue);
                                foreach (var rule in rules)
                                {
                                    if (!rule(mgrData))
                                    {
                                        result.Add(data);
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                    case "String":
                    case "string":
                        {
                            var manager = TypewiseRuleCollection[1] as RulesManager<string>;
                            var rules = manager.GetRules(data.Signal);
                            if (null != rules)
                            {
                                var mgrData = new Data<string>(data.Signal, data.Value);
                                foreach (var rule in rules)
                                {
                                    if (!rule(mgrData))
                                    {
                                        result.Add(data);
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                    case "Datetime":
                    case "DateTime":
                        {
                            var manager = TypewiseRuleCollection[2] as RulesManager<DateTime>;
                            var rules = manager.GetRules(data.Signal);
                            if (null != rules)
                            {
                                var mgrData = new Data<DateTime>(data.Signal, DateTime.Parse(data.Value));
                                foreach (var rule in rules)
                                {
                                    if (!rule(mgrData))
                                    {
                                        result.Add(data);
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                    default:
                        {
                            throw new ApplicationException("Invalid value type!");
                        }
                }
            }

            return result;
        }

        public void ReloadFromFile(string xmlFilePath)
        {
            Initialize();

            var parsedRulesCollection = RulesFileParser.RulesFileParser.Parse(xmlFilePath);

            foreach (var parsedRule in parsedRulesCollection.Rules)
            {
                switch (parsedRule.Type)
                {
                    case "int":
                        {
                            var rule = new Rule<int>(
                                parsedRule.Signal,
                                parsedRule.Operator,
                                int.Parse(parsedRule.Value));
                            var collection = (RulesManager<int>)TypewiseRuleCollection[0];
                            collection.AddRule(rule);
                            break;
                        }
                    case "string":
                        {
                            var rule = new Rule<string>(
                                parsedRule.Signal,
                                parsedRule.Operator,
                                parsedRule.Value);
                            var collection = (RulesManager<string>)TypewiseRuleCollection[1];
                            collection.AddRule(rule);
                            break;
                        }
                    case "DateTime":
                        {
                            DateTime dateTime;
                            if (String.Compare(parsedRule.Value, "now", true) == 0)
                            {
                                dateTime = DateTime.Now;
                            }
                            else
                            {
                                dateTime = DateTime.Parse(parsedRule.Value);
                            }
                            var rule = new Rule<DateTime>(
                                parsedRule.Signal,
                                parsedRule.Operator,
                                dateTime);
                            var collection = (RulesManager<DateTime>)TypewiseRuleCollection[2];
                            collection.AddRule(rule);
                            break;
                        }
                    default:
                        throw new ApplicationException("Invalid data type!");
                }
            }
        }

        private void Initialize()
        {
            TypewiseRuleCollection = new object[SupportedTypes.Length];
            var index = 0;
            foreach (var type in SupportedTypes)
            {
                var rulesCollectionType = typeof(RulesManager<>);
                TypewiseRuleCollection[index] = Activator.CreateInstance(rulesCollectionType.MakeGenericType(new Type[] { type }));
                index++;
            }
        }
    }
}

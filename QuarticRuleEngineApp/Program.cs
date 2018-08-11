using System;
using QuarticRuleEngine;

namespace QuarticRuleEngineApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new RulesEngine("RulesCollection.xml");
            var result = engine.Run("raw_data.json");

            foreach (var item in result)
            {
                Console.WriteLine($"signal: {item.Signal}, value: {item.Value}, value_type: {item.ValueType}");
            }

            Console.ReadLine();
        }
    }
}

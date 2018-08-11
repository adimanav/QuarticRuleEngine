using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace QuarticRuleEngine.RulesManager
{
    internal class RulesManager<T>
    {
        private Dictionary<string, List<Func<Data<T>, bool>>> RulesMap
        {
            get;
        }

        public RulesManager()
        {
            RulesMap = new Dictionary<string, List<Func<Data<T>, bool>>>();
        }

        public RulesManager(int capacity)
        {
            RulesMap = new Dictionary<string, List<Func<Data<T>, bool>>>(capacity);
        }

        public void AddRule(Rule<T> r)
        {
            var param = Expression.Parameter(typeof(Data<T>));

            var left = Expression.Property(param, "Value");
            var tProp = typeof(T);
            ExpressionType tBinary;
            Expression expr;
            // is the operator a known .NET operator?
            if (ExpressionType.TryParse(r.Operator, out tBinary))
            {
                var right = Expression.Constant(r.Value);
                // use a binary operation, e.g. 'Equal' -> 'u.Age == 15'
                expr = Expression.MakeBinary(tBinary, left, right);
            }
            else
            {
                var method = tProp.GetMethod(r.Operator);
                var tParam = method.GetParameters()[0].ParameterType;
                var right = Expression.Constant(r.Value);
                // use a method call, e.g. 'Contains' -> 'u.Tags.Contains(some_tag)'
                expr = Expression.Call(left, method, right);
            }

            if (!RulesMap.ContainsKey(r.Signal))
            {
                RulesMap[r.Signal] = new List<Func<Data<T>, bool>>();
            }

            RulesMap[r.Signal].Add(Expression.Lambda<Func<Data<T>, bool>>(expr, param).Compile());
        }

        public Func<Data<T>, bool>[] GetRules(string signal)
        {
            if (!RulesMap.ContainsKey(signal))
            {
                return null;
            }

            return RulesMap[signal].ToArray();
        }
    }
}

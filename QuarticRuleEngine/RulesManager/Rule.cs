
namespace QuarticRuleEngine.RulesManager
{
    internal class Rule<T>
    {
        public string Signal
        {
            get;
            set;
        }

        public string Operator
        {
            get;
            set;
        }

        public T Value
        {
            get;
            set;
        }

        public Rule(string signal, string op, T value)
        {
            this.Signal = signal;
            this.Operator = op;
            this.Value = value;
        }
    }
}

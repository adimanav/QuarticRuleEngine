namespace QuarticRuleEngine.RulesManager
{
    internal class Data<T>
    {
        public string Signal
        {
            get;
            set;
        }

        public T Value
        {
            get;
            set;
        }

        public Data(string signal, T value)
        {
            Signal = signal;
            Value = value;
        }
    }
}
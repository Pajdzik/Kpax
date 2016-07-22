namespace Eks.Core.Network.Uri
{
    public class QueryParameter
    {
        public QueryParameter(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        public string Key { get; }

        public string Value { get; }

        public override string ToString()
        {
            return $"{this.Key}={this.Value}";
        }
    }
}
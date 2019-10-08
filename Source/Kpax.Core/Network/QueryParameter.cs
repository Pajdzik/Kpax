using System.Globalization;

namespace Kpax.Core.Network
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
            return string.Format(CultureInfo.InvariantCulture, "{0}={1}", this.Key, this.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            return this.Equals((QueryParameter) obj);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        protected bool Equals(QueryParameter other)
        {
            return string.Equals(this.Key, other.Key) && string.Equals(this.Value, other.Value);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((this.Key?.GetHashCode() ?? 0)*397) ^
                       (this.Value?.GetHashCode() ?? 0);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using Kpax.Core.Verification;

namespace Kpax.Core.Network
{
    /// <summary>
    /// Builds an URI in following format
    /// scheme:[//[user:password@]host[:port]][/]path[?query][#fragment]
    /// </summary>
    public class UriCreator
    {
        private readonly IList<string> paths = new List<string>();
        private readonly IList<QueryParameter> queryParameters = new List<QueryParameter>();

        private UriCreator()
        {
        }

        private UriCreator(Uri uri)
        {
            this.Fragment = uri.Fragment;
            this.Host = uri.Host;
            this.Port = (uri.Port == -1 ? null : (int?) uri.Port);
            this.Scheme = uri.Scheme;
            this.queryParameters = ParseParameters(uri.Query);
            this.paths = ParsePaths(uri.AbsolutePath);
            this.InitUsernameAndPassword(uri.UserInfo);
        }

        public IReadOnlyCollection<string> Paths
            => new ReadOnlyCollection<string>(this.paths);

        public IReadOnlyCollection<QueryParameter> QueryParameters
            => new ReadOnlyCollection<QueryParameter>(this.queryParameters);

        public string Fragment { get; set; }

        public string Host { get; set; }

        public string ParameterDelimiter { get; set; } = "&";

        public string Password { get; set; }

        public int? Port { get; set; }

        public string Scheme { get; set; }

        public string User { get; set; }

        private static IList<string> ParsePaths(string absolutePath)
        {
            if (string.IsNullOrWhiteSpace(absolutePath))
            {
                return new List<string>();
            }

            return absolutePath.Split(new [] {'/'}, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public static UriCreator Create()
        {
            return new UriCreator();
        }

        private void InitUsernameAndPassword(string userInfo)
        {
            if (userInfo.Length <= 0) return;
            var index = userInfo.IndexOf(':');

            if (index != -1)
            {
                this.Password = userInfo.Substring(index + 1);
                this.User = userInfo.Substring(0, index);
            }
            else
            {
                this.User = userInfo;
            }
        }

        public static UriCreator FromUri(Uri uri)
        {
            return new UriCreator(uri);
        }

        private static IList<QueryParameter> ParseParameters(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return new List<QueryParameter>();
            }

            var queryParameters = query.Substring(1).Split('&');
            var parameters = queryParameters.Select(param =>
            {
                var parts = param.Split('=');
                return new QueryParameter(parts[0], parts[1]);
            });

            return parameters.ToList();
        }

        public UriCreator WithHost(string host)
        {
            this.Host = host;
            return this;
        }

        public UriCreator WithScheme(string scheme)
        {
            this.Scheme = scheme;
            return this;
        }

        public UriCreator WithUser(string user)
        {
            this.User = user;
            return this;
        }

        public UriCreator WithPassword(string password)
        {
            this.Password = password;
            return this;
        }

        public UriCreator AddPath(string path)
        {
            this.paths.Add(path);
            return this;
        }

        public UriCreator AddQueryParameter(string key, string value)
        {
            return this.AddQueryParameter(new QueryParameter(key, value));
        }

        public UriCreator AddQueryParameter(QueryParameter parameter)
        {
            this.queryParameters.Add(parameter);
            return this;
        }

        public UriCreator WithParametersDelimiter(string parameterDelimiter)
        {
            this.ParameterDelimiter = parameterDelimiter;
            return this;
        }

        public UriCreator WithPort(int port)
        {
            this.Port = port;
            return this;
        }

        public UriCreator WithFragment(string fragment)
        {
            this.Fragment = fragment;
            return this;
        }

        public Uri BuildUri()
        {
            return new Uri(this.BuildString());
        }

        public string BuildString()
        {
            Throws.IfNull(this.Host, nameof(this.Host), Resources.EveryUriHasToHaveAProperHost);
            return this.ToString();
        }

        public override string ToString()
        {
            var uri = new StringBuilder();

            this.AppendScheme(uri);
            uri.Append("//");
            this.AppendUserAndPassword(uri);
            this.AppendHost(uri);
            this.AppendPort(uri);
            this.AppendSlash(uri);
            this.AppendPaths(uri);
            this.AppendParameters(uri);
            this.AppendFragment(uri);

            return uri.ToString();
        }

        //public UriCreator DeepClone()
        //{
        //    var clone = (UriCreator) this.MemberwiseClone();
        //    clone.
        //}

        private void AppendSlash(StringBuilder uri)
        {
            if (this.paths.Any() || this.queryParameters.Any() || this.Fragment != null)
            {
                uri.Append("/");
            }
        }

        private void AppendParameters(StringBuilder uri)
        {
            if (!this.queryParameters.Any())
            {
                return;
            }

            uri.Append("?");

            foreach (var parameter in this.queryParameters)
            {
                if (!parameter.Equals(this.queryParameters.First()))
                {
                    uri.Append(this.ParameterDelimiter);
                }

                uri.Append(string.Format(CultureInfo.InvariantCulture, "{0}={1}", parameter.Key, parameter.Value));
            }
        }

        private void AppendFragment(StringBuilder uri)
        {
            if (this.Fragment != null)
            {
                uri.Append(string.Format(CultureInfo.InvariantCulture, "#{0}", this.Fragment));
            }
        }

        private void AppendPaths(StringBuilder uri)
        {
            if (!this.paths.Any())
            {
                return;
            }

            foreach (var path in this.paths)
            {
                if (path != this.paths.First())
                {
                    uri.Append("/");
                }

                uri.Append(path);
            }
        }

        private void AppendPort(StringBuilder uri)
        {
            if (this.Port != null)
            {
                uri.Append(string.Format(CultureInfo.InvariantCulture, ":{0}", this.Port));
            }
        }

        private void AppendHost(StringBuilder uri)
        {
            uri.Append(this.Host);
        }

        private void AppendUserAndPassword(StringBuilder uri)
        {
            if (this.User == null && this.Password == null)
            {
                return;
            }

            if (this.User == null && this.Password != null)
            {
                throw new ArgumentException("Cannot specify password for empty user");
            }

            if (this.User != null)
            {
                uri.Append(this.User);
            }

            if (this.Password != null)
            {
                uri.Append(string.Format(CultureInfo.InvariantCulture, ":{0}", this.Password));
            }

            uri.Append("@");
        }

        private void AppendScheme(StringBuilder uri)
        {
            if (this.Scheme != null)
            {
                uri.Append(string.Format(CultureInfo.InvariantCulture, "{0}:", this.Scheme));
            }
        }
    }
}
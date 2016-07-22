using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eks.Core.Verification;

namespace Eks.Core.Network.Uri
{
    /// <summary>
    /// Builds an URI in following format
    /// scheme:[//[user:password@]host[:port]][/]path[?query][#fragment]
    /// </summary>
    public class UriCreator
    {
        private readonly IList<QueryParameter> parameters = new List<QueryParameter>();

        private readonly IList<string> paths = new List<string>();

        private string fragment = null;

        private string host = null;

        private string parameterDelimiter = "&";

        private string password = null;

        private int? port = null;

        private string scheme = null;

        private string user = null;

        private UriCreator()
        {
        }

        public static UriCreator Create()
        {
            return new UriCreator();
        }

        public UriCreator WithHost(string host)
        {
            this.host = host;
            return this;
        }

        public UriCreator WithScheme(string scheme)
        {
            this.scheme = scheme;
            return this;
        }

        public UriCreator WithUser(string user)
        {
            this.user = user;
            return this;
        }

        public UriCreator WithPassword(string password)
        {
            this.password = password;
            return this;
        }

        public UriCreator AddPath(string path)
        {
            this.paths.Add(path);
            return this;
        }

        public UriCreator AddParameter(string key, string value)
        {
            return this.AddParameter(new QueryParameter(key, value));
        }

        public UriCreator AddParameter(QueryParameter parameter)
        {
            this.parameters.Add(parameter);
            return this;
        }

        public UriCreator WithParamDelimiter(string parameterDelimiter)
        {
            this.parameterDelimiter = parameterDelimiter;
            return this;
        }

        public UriCreator WithPort(int port)
        {
            this.port = port;
            return this;
        }

        public UriCreator WithFragment(string fragment)
        {
            this.fragment = fragment;
            return this;
        }

        public System.Uri BuildUri()
        {
            return new System.Uri(this.BuildString());
        }

        public string BuildString()
        {
            Throw.IfNull(this.host, nameof(this.host), "Every URI has to have a proper host");
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

        private void AppendSlash(StringBuilder uri)
        {
            if (this.paths.Any() || this.parameters.Any() || this.fragment != null)
            {
                uri.Append("/");
            }
        }

        private void AppendParameters(StringBuilder uri)
        {
            if (!this.parameters.Any())
            {
                return;
            }

            uri.Append("?");

            foreach (var parameter in this.parameters)
            {
                if (!parameter.Equals(this.parameters.First()))
                {
                    uri.Append(this.parameterDelimiter);
                }

                uri.Append($"{parameter.Key}={parameter.Value}");
            }
        }

        private void AppendFragment(StringBuilder uri)
        {
            if (this.fragment != null)
            {
                uri.Append($"#{this.fragment}");
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

                uri.Append($"{path}");
            }
        }

        private void AppendPort(StringBuilder uri)
        {
            if (this.port != null)
            {
                uri.Append($":{this.port}");
            }
        }

        private void AppendHost(StringBuilder uri)
        {
            uri.Append(this.host);
        }

        private void AppendUserAndPassword(StringBuilder uri)
        {
            if (this.user == null && this.password == null)
            {
                return;
            }

            if (this.user == null && this.password != null)
            {
                throw new ArgumentException("Cannot specify password for empty user");
            }

            if (this.user != null)
            {
                uri.Append(this.user);
            }

            if (this.password != null)
            {
                uri.Append($":{this.password}");
            }

            uri.Append("@");
        }

        private void AppendScheme(StringBuilder uri)
        {
            if (this.scheme != null)
            {
                uri.Append($"{this.scheme}:");
            }
        }
    }
}
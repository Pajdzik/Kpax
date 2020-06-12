using System;
using Kpax.Core.Network;
using FluentAssertions;
using Xunit;

namespace Kpax.Core.UnitTests.Network
{
    public class UriCreatorTests
    {
        public class HostTests
        {
            [Fact]
            public void HostNotSet_ExceptionThrown()
            {
                Action action = () => UriCreator.Create().BuildUri();
                action.Should().Throw<ArgumentNullException>();
            }

            [Fact]
            public void HostSet_PropertUriReturned()
            {
                var uri = UriCreator.Create().WithHost("testhost").BuildString();
                uri.Should().Be("//testhost");
            }
        }

        public class PortTest
        {
            [Fact]
            public void PortSet_ProperUriReturned()
            {
                var uri = UriCreator.Create().WithHost("test").WithPort(1234).BuildString();
                uri.Should().Be("//test:1234");
            }
        }

        public class UserAndPasswordTests
        {
            [Fact]
            public void EmptyUserPasswordSet_ExceptionThrown()
            {
                Action action = () => UriCreator.Create().WithHost("test").WithPassword("pass").BuildString();
                action.Should().Throw<ArgumentException>();
            }

            [Fact]
            public void UserSet_ProperUriReturned()
            {
                var uri = UriCreator.Create().WithHost("test").WithUser("user").BuildString();
                uri.Should().Be("//user@test");
            }

            [Fact]
            public void UserSetPasswordSet_ProperUriReturned()
            {
                var uri = UriCreator.Create().WithHost("test").WithUser("user").WithPassword("pass").BuildString();
                uri.Should().Be("//user:pass@test");
            }
        }

        public class SchemeTests
        {
            [Fact]
            public void SchemeSet_ProperUriReturned()
            {
                var uri = UriCreator.Create().WithHost("host").WithScheme("scheme").BuildString();
                uri.Should().Be("scheme://host");
            }
        }

        public class PathsTests
        {
            [Fact]
            public void OnePathAdded_ProperUriReturned()
            {
                var uri = UriCreator.Create().WithHost("host").AddPath("path").BuildString();
                uri.Should().Be("//host/path");
            }

            [Fact]
            public void TwoPathsAdded_ProperUriReturned()
            {
                var uri = UriCreator.Create()
                    .WithHost("host")
                    .AddPath("path1")
                    .AddPath("path2")
                    .BuildString();

                uri.Should().Be("//host/path1/path2");
            }
        }

        public class ParametersTests
        {
            [Fact]
            public void OneParameterAdded_ProperUriReturned()
            {
                var uri = UriCreator.Create().WithHost("host").AddQueryParameter("param", "value").BuildString();
                uri.Should().Be("//host/?param=value");
            }

            [Fact]
            public void OneParameterWithPathAdded_ProperUriReturned()
            {
                var uri = UriCreator.Create()
                    .WithHost("host")
                    .AddPath("path")
                    .AddQueryParameter("param", "value")
                    .BuildString();
                uri.Should().Be("//host/path?param=value");
            }

            [Fact]
            public void TwoParametersWithPathAdded_ProperUriReturned()
            {
                var uri = UriCreator.Create()
                    .WithHost("host")
                    .AddPath("path")
                    .AddQueryParameter("param", "value")
                    .AddQueryParameter(new QueryParameter("param2", "value2"))
                    .BuildString();
                uri.Should().Be("//host/path?param=value&param2=value2");
            }
        }

        public class FragmentTests
        {
            [Fact]
            public void FragmentSet_ProperUriReturned()
            {
                var uri = UriCreator.Create().WithHost("host").WithFragment("fragment").BuildString();
                uri.Should().Be("//host/#fragment");
            }
        }

        public class CtroUriTests
        {
            [Fact]
            public void UriHostOnly_HostSet()
            {
                var uri = new Uri("file://abcd");
                var uriCreator = UriCreator.FromUri(uri);
                uriCreator.Host.Should().Be("abcd");
            }

            [Fact]
            public void UriSchemeHostOnly_SchemeSet()
            {
                var uri = new Uri("http://abcd");
                var uriCreator = UriCreator.FromUri(uri);
                uriCreator.Host.Should().Be("abcd");
                uriCreator.Scheme.Should().Be("http");
            }

            [Fact]
            public void UriHostPortOnly_PortSet()
            {
                var uri = new Uri("prot://abcd:1234/", UriKind.Absolute);
                var uriCreator = UriCreator.FromUri(uri);
                uriCreator.Host.Should().Be("abcd");
                uriCreator.Port.Should().Be(1234);
            }

            [Fact]
            public void UriHostPathsOnly_PathsSet()
            {
                var uri = new Uri("//abcd/path1/path2");
                var uriCreator = UriCreator.FromUri(uri);
                uriCreator.Paths.Should().ContainInOrder("path1", "path2");
            }

            [Fact]
            public void UriHostQueryOnly_QuerySet()
            {
                var uri = new Uri("http://abcd/path?param1=a&param2=b");
                var uriCreator = UriCreator.FromUri(uri);
                uriCreator.QueryParameters.Count.Should().Be(2);
                uriCreator.QueryParameters.Should()
                    .ContainInOrder(new QueryParameter("param1", "a"), new QueryParameter("param2", "b"));
            }
        }

        [Fact]
        public void EveryParameterPassed_ProperUriReturned()
        {
            var uri = UriCreator.Create()
                .WithHost("host")
                .AddPath("path1")
                .AddPath("path2")
                .AddQueryParameter("param1", "val1")
                .AddQueryParameter("param2", "val2")
                .WithPort(1234)
                .WithUser("user")
                .WithPassword("pass")
                .WithScheme("scheme")
                .BuildString();

            uri.Should
                ().
                Be("scheme://user:pass@host:1234/path1/path2?param1=val1&param2=val2");
        }

        [Fact]
        public void RapGeniusUri()
        {
            var uri = new Uri("https://api.genius.com/search?q=Kendrick%20Lamar");
            var actualUri = UriCreator.FromUri(uri).BuildUri();

            actualUri.Should().Be(uri);
        }
    }
}
using System;
using Eks.Core.Network.Uri;
using FluentAssertions;
using Xunit;

namespace Eks.Core.UnitTests.Network.Uri
{
    public class UriCreatorTests
    {
        public class HostTests
        {
            [Fact]
            public void HostNotSet_ExceptionThrown()
            {
                Action action = () => UriCreator.Create().BuildUri();
                action.ShouldThrow<ArgumentNullException>();
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
                action.ShouldThrow<ArgumentException>();
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
                var uri = UriCreator.Create().WithHost("host").AddParameter("param", "value").BuildString();
                uri.Should().Be("//host/?param=value");
            }

            [Fact]
            public void OneParameterWithPathAdded_ProperUriReturned()
            {
                var uri = UriCreator.Create()
                    .WithHost("host")
                    .AddPath("path")
                    .AddParameter("param", "value")
                    .BuildString();
                uri.Should().Be("//host/path?param=value");
            }

            [Fact]
            public void TwoParametersWithPathAdded_ProperUriReturned()
            {
                var uri = UriCreator.Create()
                    .WithHost("host")
                    .AddPath("path")
                    .AddParameter("param", "value")
                    .AddParameter(new QueryParameter("param2", "value2"))
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
                uri.Should().Be("scheme://host");
            }
        }

        [Fact]
        public void EveryParameterPassed_ProperUriReturned()
        {
            var uri = UriCreator.Create()
                .WithHost("host")
                .AddPath("path1")
                .AddPath("path2")
                .AddParameter("param1", "val1")
                .AddParameter("param2", "val2")
                .WithPort(1234)
                .WithUser("user")
                .WithPassword("pass")
                .WithScheme("scheme")
                .BuildString();

            uri.Should().Be("scheme://user:pass@host:1234/path1/path2?param1=val1&param2=val2");
        }
    }
}
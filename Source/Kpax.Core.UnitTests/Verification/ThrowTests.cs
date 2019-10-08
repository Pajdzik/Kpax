using FluentAssertions;
using System;
using Kpax.Core.Verification;
using Xunit;

namespace Kpax.Core.UnitTests.Verification
{
    public class ThrowTests
    {
        public class IfNullTests
        {
            [Fact]
            public void IsNull_ExceptionThrown()
            {
                Action action = () => Throws.IfNull(null);
                action.Should().Throw<ArgumentNullException>().And.ParamName.Should().BeNull();
            }

            [Fact]
            public void SimpleType_NoExceptionThrown()
            {
                Action action = () => Throws.IfNull(0);
                action.Should().NotThrow<ArgumentNullException>();
            }

            [Fact]
            public void ObjectType_NoExceptionThrown()
            {
                Action action = () => Throws.IfNull(new object());
                action.Should().NotThrow<ArgumentNullException>();
            }
        }

        public class IfNullParameterNameTests
        {
            [Fact]
            public void IsNull_ExceptionThrown()
            {
                object parameter = null;
                Action action = () => Throws.IfNull(parameter, nameof(parameter));
                action.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be(nameof(parameter));
            }

            [Fact]
            public void SimpleType_NoExceptionThrown()
            {
                var parameter = 0;
                Action action = () => Throws.IfNull(parameter, nameof(parameter));
                action.Should().NotThrow<ArgumentNullException>();
            }

            [Fact]
            public void ObjectType_NoExceptionThrown()
            {
                var parameter = new object();
                Action action = () => Throws.IfNull(parameter, nameof(parameter));
                action.Should().NotThrow<ArgumentNullException>();
            }
        }

        public class IfNullParameterNamMessageeTests
        {
            [Fact]
            public void IsNull_ExceptionThrown()
            {
                object parameter = null;
                Action action = () => Throws.IfNull(parameter, nameof(parameter), "Parameter should not be null");
                action.Should().Throw<ArgumentNullException>().And.Message.Should().Be("Parameter should not be null (Parameter 'parameter')");
            }
        }
    }
}

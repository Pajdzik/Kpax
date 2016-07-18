using FluentAssertions;
using System;
using Eks.Core.Verification;
using Xunit;

namespace Eks.Core.UnitTests.Verification
{
    public class ThrowTests
    {
        public class IfNullTests
        {
            [Fact]
            public void IsNull_ExceptionThrown()
            {
                Action action = () => Throw.IfNull(null);
                action.ShouldThrow<ArgumentNullException>().And.ParamName.Should().BeNull();
            }

            [Fact]
            public void SimpleType_NoExceptionThrown()
            {
                Action action = () => Throw.IfNull(0);
                action.ShouldNotThrow<ArgumentNullException>();
            }

            [Fact]
            public void ObjectType_NoExceptionThrown()
            {
                Action action = () => Throw.IfNull(new object());
                action.ShouldNotThrow<ArgumentNullException>();
            }
        }

        public class IfNullParameterNameTests
        {
            [Fact]
            public void IsNull_ExceptionThrown()
            {
                object parameter = null;
                Action action = () => Throw.IfNull(parameter, nameof(parameter));
                action.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be(nameof(parameter));
            }

            [Fact]
            public void SimpleType_NoExceptionThrown()
            {
                var parameter = 0;
                Action action = () => Throw.IfNull(parameter, nameof(parameter));
                action.ShouldNotThrow<ArgumentNullException>();
            }

            [Fact]
            public void ObjectType_NoExceptionThrown()
            {
                var parameter = new object();
                Action action = () => Throw.IfNull(parameter, nameof(parameter));
                action.ShouldNotThrow<ArgumentNullException>();
            }
        }

        public class IfNullParameterNamMessageeTests
        {
            [Fact]
            public void IsNull_ExceptionThrown()
            {
                object parameter = null;
                Action action = () => Throw.IfNull(parameter, nameof(parameter), "Parameter should not be null");
                action.ShouldThrow<ArgumentNullException>().And.Message.Should().Be("Parameter should not be null\r\nParameter name: parameter");
            }
        }
    }
}

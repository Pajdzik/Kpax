using FluentAssertions;
using System;
using Eks.Core.Verification;
using Xunit;

namespace Eks.Core.UnitTests.Verification
{
    public class VerifyTests
    {
        public class IsNullTests
        {
            [Fact]
            public void IsNull_ExceptionThrown()
            {
                Action action = () => Verify.IsNull(null);
                action.ShouldThrow<ArgumentNullException>().And.ParamName.Should().BeNull();
            }

            [Fact]
            public void SimpleType_NoExceptionThrown()
            {
                Action action = () => Verify.IsNull(0);
                action.ShouldNotThrow<ArgumentNullException>();
            }

            [Fact]
            public void ObjectType_NoExceptionThrown()
            {
                Action action = () => Verify.IsNull(new object());
                action.ShouldNotThrow<ArgumentNullException>();
            }
        }

        public class IsNullParameterNameTests
        {
            [Fact]
            public void IsNull_ExceptionThrown()
            {
                object parameter = null;
                Action action = () => Verify.IsNull(parameter, nameof(parameter));
                action.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be(nameof(parameter));
            }

            [Fact]
            public void SimpleType_NoExceptionThrown()
            {
                var parameter = 0;
                Action action = () => Verify.IsNull(parameter, nameof(parameter));
                action.ShouldNotThrow<ArgumentNullException>();
            }

            [Fact]
            public void ObjectType_NoExceptionThrown()
            {
                var parameter = new object();
                Action action = () => Verify.IsNull(parameter, nameof(parameter));
                action.ShouldNotThrow<ArgumentNullException>();
            }
        }

        public class IsNullParameterNamMessageeTests
        {
            [Fact]
            public void IsNull_ExceptionThrown()
            {
                object parameter = null;
                Action action = () => Verify.IsNull(parameter, nameof(parameter), "Parameter should not be null");
                action.ShouldThrow<ArgumentNullException>().And.Message.Should().Be("Parameter should not be null\r\nParameter name: parameter");
            }
        }
    }
}

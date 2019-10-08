using Kpax.Core.Maths;
using FluentAssertions;
using Xunit;

namespace Kpax.Core.UnitTests.Math
{
    public class RoundingExtensionsTests
    {
        public class RoundToSignificantDigitsIntTests
        {
            [Fact]
            public void RoundToSignificantDigits_ReturnsZero_WhenZeroPassed()
            {
                int zero = 0;
                var result = RoundingExtensions.RoundToSignificantDigits(zero, 1);

                result.Should().Be(0);
            }

            [Fact]
            public void RoundToSignificantDigits_ReturnsFullNumber_WhenTooManySignificantDigitsPassed()
            {
                int digit = 123;
                var result = RoundingExtensions.RoundToSignificantDigits(digit, 3);

                result.Should().Be(123);
            }

            [Fact]
            public void RoundToSignificantDigits_ReturnsThreeSignificantDigits_WhenThreeSignificantDigitsPassed()
            {
                int digit = 123456;
                var result = RoundingExtensions.RoundToSignificantDigits(digit, 3);

                result.Should().Be(123000);
            }

            [Fact]
            public void RoundToSignificantDigits_ReturnsThreeSignificantDigits_WhenNineDigitsPassed()
            {
                int digit = 123456789;
                var result = RoundingExtensions.RoundToSignificantDigits(digit);

                result.Should().Be(123000000);
            }

            [Fact]
            public void RoundToSignificantDigits_ReturnsThreeSignificantDigits_WhenNineDigitsPassed2()
            {
                int digit = 1020000;
                var result = RoundingExtensions.RoundToSignificantDigits(digit);

                result.Should().Be(1000000);
            }
        }
    }
}
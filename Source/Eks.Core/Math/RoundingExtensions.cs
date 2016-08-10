using System;

namespace Eks.Core.Maths
{
    public static class RoundingExtensions
    {
        public static int RoundToSignificantDigits(this int digit, int significantDigits)
        {
            return (int)RoundingExtensions.RoundToSignificantDigits((double)digit, significantDigits);
        }

        public static long RoundToSignificantDigits(this long digit, int significantDigits)
        {
            return (long) RoundingExtensions.RoundToSignificantDigits((double) digit, significantDigits);
        }

        public static double RoundToSignificantDigits(this double digit, int significantDigits)
        {
            return digit.RoundToSignificantDigits(significantDigits, 0.01);
        }

        public static double RoundToSignificantDigits(this double digit, int significantDigits, double delta)
        {
            if (Math.Abs(digit) < delta)
            {
                return 0;
            }

            var numberOfDigits = Math.Floor(Math.Log10(Math.Abs(digit))) + 1;
            var scale = Math.Pow(10, numberOfDigits);

            return scale*Math.Round(digit/scale, significantDigits);
        }
    }
}
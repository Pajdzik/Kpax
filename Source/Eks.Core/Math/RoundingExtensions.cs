using System;

namespace Eks.Core.Maths
{
    public static class RoundingExtensions
    {
        public static int RoundToSignificantDigits(this int number)
        {
            var digits = GetNumberDigitCount(number)/3;
            return (int)RoundingExtensions.RoundToSignificantDigits((double)number, digits);
        }


        public static int RoundToSignificantDigits(this int number, int significantDigits)
        {
            return (int)RoundingExtensions.RoundToSignificantDigits((double)number, significantDigits);
        }

        public static long RoundToSignificantDigits(this long number, int significantDigits)
        {
            return (long) RoundingExtensions.RoundToSignificantDigits((double) number, significantDigits);
        }

        public static double RoundToSignificantDigits(this double number, int significantDigits)
        {
            return number.RoundToSignificantDigits(significantDigits, 0.01);
        }

        public static double RoundToSignificantDigits(this double number, int significantDigits, double delta)
        {
            if (Math.Abs(number) < delta)
            {
                return 0;
            }

            var numberOfDigits = Math.Floor(Math.Log10(Math.Abs(number))) + 1;
            var scale = Math.Pow(10, numberOfDigits);

            return scale*Math.Round(number/scale, significantDigits);
        }

        private static int GetNumberDigitCount(int number)
        {
            if (number == 0)
            {
                return 1;
            }

            return (int) (Math.Floor(Math.Log10(Math.Abs(number))) + 1);
        }
    }
}
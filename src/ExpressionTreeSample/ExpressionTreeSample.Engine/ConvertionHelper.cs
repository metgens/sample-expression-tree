namespace ExpressionTreeSample.Engine
{
    internal static class ConversionHelper
    {
        internal static double ConvertToDouble(string value)
        {
            if (value == null)
            {
                return 0;
            }
            else
            {
                if (!double.TryParse(value, out double outVal))
                    throw new ArgumentException($"Wrong number format for parameter: '{value}'");

                if (double.IsNaN(outVal) || double.IsInfinity(outVal))
                    throw new ArgumentException($"Wrong number format for parameter: '{value}'");

                return outVal;
            }
        }

        internal static int ConvertToInt(string value)
        {
            if (value == null)
            {
                return 0;
            }
            else
            {
                if (!int.TryParse(value, out int outVal))
                    throw new ArgumentException($"Wrong number format for parameter: '{value}'");

                return outVal;
            }
        }
    }
}
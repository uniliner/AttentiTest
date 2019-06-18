using System;

namespace AttentiTests
{
    public static class Helper
    {
        public static int GetFractionLength(double num)
        {
            string resultStr = num.ToString();
            if(resultStr.IndexOf(".") != -1) {
                return resultStr.Split(".")[1].Length;
            } else {
                return 0;
            }
        }

        public enum Units
        {
            Celsius,
            Fahrenheit,
            Meters,
            Feet,
            Ounces,
            Grams
        }
    }
}
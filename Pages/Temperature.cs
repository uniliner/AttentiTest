using System;

namespace AttentiTests.Pages
{
    public class Temperature : Page
    {
        public Temperature(): base("temperature") {}

        override public double CalculateExpect(int input, Helper.Units from, Helper.Units to, int fractionLength)
        {
            return Math.Round(LocateMethod(from, to)(input), fractionLength);
        }

        private ConvertMethod LocateMethod(Helper.Units from, Helper.Units to)
        {
            switch(from)
            {
                case Helper.Units.Celsius:
                    switch(to)
                    {
                        case Helper.Units.Fahrenheit:
                            return new ConvertMethod(CelciusToFahrenheit);
                        default:
                            throw new UnitArgumentException(to);
                    }
                case Helper.Units.Fahrenheit:
                    switch(to)
                    {
                        case Helper.Units.Celsius:
                            return new ConvertMethod(CelciusToFahrenheit);
                        default:
                            throw new UnitArgumentException(to);
                    }
                default:
                    throw new UnitArgumentException(from);
            }
        }

        public double CelciusToFahrenheit(double input)
        {
            return input * 1.8000 + 32.00;
        }

        public double FahrenheitToCelcius(double input)
        {
            return (input - 32) * 5/9.0;
        }
    }
}
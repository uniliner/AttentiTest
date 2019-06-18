using System;

namespace AttentiTests.Pages
{
    public class Weight : Page
    {
        public Weight(): base("weight") {}

        override public double CalculateExpect(int input, Helper.Units from, Helper.Units to, int fractionLength)
        {
            return Math.Round(OuncesToGrams(input), fractionLength);
        }

        private ConvertMethod LocateMethod(Helper.Units from, Helper.Units to)
        {
            switch(from)
            {
                case Helper.Units.Celsius:
                    switch(to)
                    {
                        case Helper.Units.Fahrenheit:
                            return new ConvertMethod(OuncesToGrams);
                        default:
                            throw new UnitArgumentException(to);
                    }
                default:
                    throw new UnitArgumentException(from);
            }
        }

        public double OuncesToGrams(double input)
        {
            return input * 28.349525F;
        }
    }
}
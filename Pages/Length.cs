using System;

namespace AttentiTests.Pages
{
    public class Length : Page
    {
        public Length(): base("length") {}

        override public double CalculateExpect(int input, Helper.Units from, Helper.Units to, int fractionLength)
        {
            return Math.Round(LocateMethod(from, to)(input), fractionLength);
        }

        private ConvertMethod LocateMethod(Helper.Units from, Helper.Units to)
        {
            switch(from)
            {
                case Helper.Units.Meters:
                    switch(to)
                    {
                        case Helper.Units.Feet:
                            return new ConvertMethod(MetersToFeet);
                        default:
                            throw new UnitArgumentException(to);
                    }
                default:
                    throw new UnitArgumentException(from);
            }
        }

        public double MetersToFeet(double input)
        {
            return input * 3.2808399F;
        }
    }
}
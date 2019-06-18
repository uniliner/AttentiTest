using System;

namespace AttentiTests
{
    public class UnitArgumentException : Exception
    {
        public UnitArgumentException(Helper.Units unit) : base(Enum.GetName(typeof(Helper.Units), unit) + " unit is not supported") {}
    }
}
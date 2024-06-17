using CreateInvoice.Enums;

namespace CreateInvoice.Extensions
{
    public static class UnitConverter
    {
        private const double Cm2ToM2Factor = 0.0001;
        private const double M2ToCm2Factor = 10000;

        public static double Convert(double value, Unit fromUnit, Unit toUnit)
        {
            if (fromUnit == toUnit)
            {
                return value;
            }

            return fromUnit switch
            {
                Unit.CM2 when toUnit == Unit.M2 => value * Cm2ToM2Factor,
                Unit.M2 when toUnit == Unit.CM2 => value * M2ToCm2Factor,
                _ => throw new NotSupportedException($"Conversion from {fromUnit} to {toUnit} is not supported.")
            };
        }
    }
}

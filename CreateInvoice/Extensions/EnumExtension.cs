using CreateInvoice.Enums;
using System.ComponentModel.DataAnnotations;

namespace CreateInvoice.Extensions
{
    public static class EnumExtension
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var displayNameAttribute = enumValue.GetType()
                .GetField(enumValue.ToString())
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .FirstOrDefault() as DisplayAttribute;

            return displayNameAttribute?.Name ?? enumValue.ToString();
        }

        public static Unit GetUnitEnum(this string displayName)
        {
            var unit = Enum.GetValues(typeof(Unit)).Cast<Unit>().FirstOrDefault(x => x.GetDisplayName() == displayName);

            if (unit == null)
            {
                throw new NotSupportedException($"Unit {displayName} is not supported.");
            }

            return unit;
        }
    }
}

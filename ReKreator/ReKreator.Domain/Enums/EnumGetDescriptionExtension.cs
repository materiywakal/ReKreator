using System;
using System.ComponentModel;
using System.Reflection;

namespace ReKreator.Domain.Enums
{
    public static class EnumGetDescriptionExtension
    {
        /// <summary>
        /// Takes enum value name from description attribute.
        /// </summary>
        /// <param name="value">Enum value</param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[]) fi.GetCustomAttributes(
                    typeof(DescriptionAttribute),
                    false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}
using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel;
using System.Reflection;

namespace MermaidDotNet.EntityFrameworkCore.Extensions
{
    public static class PropertyExtensions
    {
        public static string GetDescription(this IProperty property)
        {
            var clrProperty = property.DeclaringType.ClrType
                .GetProperty(property.Name, BindingFlags.Public | BindingFlags.Instance);

            if (clrProperty == null)
                return string.Empty;

            var descriptionAttribute = clrProperty
                .GetCustomAttribute<DescriptionAttribute>();

            return descriptionAttribute?.Description ?? string.Empty;
        }
    }
}
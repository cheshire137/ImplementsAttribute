using System.Reflection;

namespace ImplementsChecker
{
    internal static class PropertyInfoExtension
    {
        public static string GetSignature(this PropertyInfo property)
        {
            return string.Format(
                "{0} {1}",
                property.PropertyType.Name,
                property.Name
            );
        }
    }
}

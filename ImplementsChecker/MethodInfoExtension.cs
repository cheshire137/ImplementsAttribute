using System;
using System.Linq;
using System.Reflection;

namespace ImplementsChecker
{
    /// <summary>
    /// Thanks to http://stackoverflow.com/questions/1312166/print-full-signature-of-a-method-from-a-methodinfo
    /// </summary>
    internal static class MethodInfoExtension
    {
        public static string GetSignature(this MethodInfo method)
        {
            string[] parameters = method.GetParameters().Select(
                param => string.Format(
                    "{0} {1}",
                    param.ParameterType.Name,
                    param.Name
                )
            ).ToArray();

            return string.Format(
                "{0} {1}({2})",
                method.ReturnType.Name,
                method.Name,
                string.Join(",", parameters)
            );
        }
    }
}

using System.Reflection;

namespace ImplementsChecker
{
    internal static class MemberInfoExtension
    {
        public static string GetSignature(this MemberInfo member)
        {
            if (member.MemberType == MemberTypes.Method)
            {
                return ((MethodInfo)member).GetSignature();
            }
            
            if (member.MemberType == MemberTypes.Property)
            {
                return ((PropertyInfo)member).GetSignature();
            }

            return null;
        }
    }
}

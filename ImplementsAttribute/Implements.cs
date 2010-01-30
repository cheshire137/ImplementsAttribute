using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace ImplementsAttribute
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property,
        AllowMultiple = true)]
    public class Implements : Attribute
    {
        public Implements(Type interfaceType)
        {
            if (!interfaceType.IsInterface)
            {
                throw new NonInterfaceTypeException(
                    "Given type " + interfaceType +
                    " is not an interface; pass an interface type only"
                );
            }

            MethodBase callingMethod = getCallingMethod();
        }

        private MethodBase getCallingMethod()
        {
            return new StackTrace().GetFrame(2).GetMethod();
        }

        private static bool methodImplementsInterface(MethodBase method,
            Type interfaceType)
        {
            return method.IsPublic &&
                classImplementsInterface(method.ReflectedType, interfaceType) &&
                interfaceRequiresMethod(interfaceType, method);
        }

        private static bool classImplementsInterface(Type classType,
            Type interfaceType)
        {
            return classType.GetInterfaces().Contains(interfaceType);
        }

        private static bool interfaceRequiresMethod(Type interfaceType,
            MethodBase method)
        {
            foreach (MemberInfo member in interfaceType.GetMembers().Where(member => member.MemberType == MemberTypes.Method))
            {
            }

            // TODO:  Fix this
            return false;
        }
    }
}

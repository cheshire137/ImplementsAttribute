using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ImplementsAttribute;

namespace ImplementsChecker
{
    class ImplementsChecker
    {
        private const string ERROR_PREFIX = "Error:  ";

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                usageInstructions();
            }

            string dllPath = args[0];

            if (!File.Exists(dllPath))
            {
                error("File does not exist at given path");
            }

            try
            {
                Assembly dll = Assembly.LoadFile(dllPath);

                foreach (MemberInfo member in getClassMembersInAssembly(dll))
                {
                    if (memberClaimsToImplementInterface(member))
                    {
                        Type interfaceType = getInterface(member);

                        if (!memberIsRequiredForInterface(member, interfaceType))
                        {
                            warnMemberNotRequiredForInterface(member,
                                interfaceType);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                error(ex.ToString());
            }
        }

        private static bool classImplementsInterface(Type classType,
            Type interfaceType)
        {
            return classType.GetInterfaces().Contains(interfaceType);
        }

        private static void error(string message)
        {
            Console.Error.WriteLine(ERROR_PREFIX + message);
            Environment.Exit(1);
        }

        private static IEnumerable<Type> getClassesInAssembly(Assembly assembly)
        {
            return assembly.GetTypes().Where(type => type.IsClass);
        }

        private static IEnumerable<MemberInfo> getClassMembersInAssembly(
            Assembly assembly
        )
        {
            foreach (Type classType in getClassesInAssembly(assembly))
            {
                foreach (MemberInfo member in classType.GetMembers())
                {
                    yield return member;
                }
            }
        }

        private static Type getInterface(ICustomAttributeProvider member)
        {
            object[] attributes = member.GetCustomAttributes(typeof(Implements),
                false);

            if (attributes.Length < 1)
            {
                error("Could not find the 'Implements' attribute on member");
            }

            Implements attribute = (Implements)attributes[0];
            return attribute.ImplementedInterface;
        }

        private static bool memberClaimsToImplementInterface(
            ICustomAttributeProvider member
        )
        {
            return member.IsDefined(typeof(Implements), false);
        }

        private static bool memberIsRequiredForInterface(MemberInfo member,
            Type interfaceType)
        {
            return classImplementsInterface(member.GetType(), interfaceType) &&
                interfaceContainsMember(interfaceType, member);
        }

        private static void warnMemberNotRequiredForInterface(MemberInfo member,
            Type interfaceType)
        {
            string className = member.GetType().Name;
            string methodName = member.Name;
            string interfaceName = interfaceType.Name;
            Console.Error.WriteLine("Method " + methodName +
                " is not required for " + className + " to implement " +
                interfaceName + "; remove the [Implements(typeof(" +
                interfaceName + "))]" + " from " + methodName);
        }

        private static bool interfaceContainsMember(Type interfaceType,
            MemberInfo member)
        {
            return interfaceType.GetMethods().Select(
                m => m.GetSignature()
            ).Contains(member.GetSignature());
        }

        private static void usageInstructions()
        {
            Console.WriteLine("Usage:  ImplementsChecker.exe pathToDllToCheck");
            Console.WriteLine("\tExample:  ImplementsChecker.exe myProject.dll");
            Environment.Exit(0);
        }
    }
}

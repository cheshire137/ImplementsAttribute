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
        public Type ImplementedInterface { get; protected set; }

        public Implements(Type interfaceType)
        {
            if (!interfaceType.IsInterface)
            {
                Console.Error.WriteLine("Given type " + interfaceType +
                    " is not an interface; pass an interface type only");
            }

            ImplementedInterface = interfaceType;
        }
    }
}

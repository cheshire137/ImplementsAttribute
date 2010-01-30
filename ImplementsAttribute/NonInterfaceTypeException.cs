using System;

namespace ImplementsAttribute
{
    public class NonInterfaceTypeException : ArgumentException
    {
        public NonInterfaceTypeException(string message) : base(message) { }
    }
}

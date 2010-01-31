using ImplementsAttribute;

namespace TestImplementsAttribute
{
    public class MyClass : IMyInterface
    {
        [Implements(typeof(IMyInterface))]
        public int Foo()
        {
            return 3;
        }

        public string FooWithString(string value)
        {
            return value;
        }
    }
}

using System;

namespace microDI.Exceptions
{
    public class TypeNotRegisteredException : Exception
    {
        public TypeNotRegisteredException(string type)
            : base(string.Format("No such registered class type for [{0}]", type))
        {
        }
    }
}

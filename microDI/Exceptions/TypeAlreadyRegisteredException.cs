using System;

namespace microDI.Exceptions
{
    public class TypeAlreadyRegisteredException : Exception
    {
        public TypeAlreadyRegisteredException(string type) 
            : base(string.Format("Type [{0}] already registered!", type))
        {
        }
    }
}

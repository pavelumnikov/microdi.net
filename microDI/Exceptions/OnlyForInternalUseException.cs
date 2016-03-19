using System;

namespace microDI.Exceptions
{
    /// <summary>
    /// The standard exception thrown when an activation service is trying to activate type-object that
    /// is primarily used for internal purposes.
    /// </summary>
    public class OnlyForInternalUseException : Exception
    {
        private const string PreformattedString = "This type[{0}] is only for internal resolving";

        /// <summary>
        /// Initializes a new instance of the <see cref="T:microDI.Exceptions.OnlyForInternalUseException" /> class 
        /// with a specified error message.
        /// </summary>
        /// <param name="type">Type of object trying to be resolved.</param>
        public OnlyForInternalUseException(Type type)
            : base(string.Format(PreformattedString, type.Name))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:microDI.Exceptions.OnlyForInternalUseException" /> class 
        /// with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="type">Type of object trying to be resolved.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) 
        /// if no inner exception is specified. 
        /// </param>
        public OnlyForInternalUseException(Type type, Exception innerException)
            : base(string.Format(PreformattedString, type.Name), innerException)
        {
        }
    }
}

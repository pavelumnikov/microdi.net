// MIT License
// -----------
//
// Copyright(c) 2016 Pavel Umnikov
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

using System;

namespace microDI.Exceptions
{
    /// <summary>
    /// The standard exception thrown when an registry service has an error on trying to set
    /// information about type to be associated with type-object.
    /// </summary>
    /// <see cref="Exception"/>
    public class TypeAlreadyRegisteredException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:microDI.Exceptions.TypeAlreadyRegisteredException" /> 
        /// class with a specified error message.
        /// </summary>
        /// <param name="type">
        /// Type that is already registered in container's register service dictionary.
        ///  </param>
        public TypeAlreadyRegisteredException(Type type) 
            : base($"Type [{type}] already registered!")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:microDI.Exceptions.TypeAlreadyRegisteredException" /> 
        /// class with a specified error message and a reference to the inner exception that is the cause of this
        /// exception.
        /// </summary>
        /// <param name="type">
        /// Type that is already registered in container's register service dictionary.
        /// </param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) 
        /// if no inner exception is specified. 
        /// </param>
        public TypeAlreadyRegisteredException(Type type, Exception innerException)
            : base($"Type [{type}] already registered!", innerException)
        {
        }
    }
}

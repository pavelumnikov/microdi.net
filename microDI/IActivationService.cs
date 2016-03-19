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

using JetBrains.Annotations;

namespace microDI
{
    /// <summary>
    /// <c>ActivationFactory</c> is an interface for instance creation. It doesn't depend on
    /// any life cycle policy, it just creates new instance of an <c>object</c> by known type.
    ///  </summary>
    public interface IActivationService
    {
        /// <summary>
        /// Creates new instance of type from registered type in IoC.
        /// </summary>
        /// <param name="registeredObject">Instance of sidecar for registered type.</param>
        /// <param name="isExternal">Ensures if we are trying to get object for external or internal use.</param>
        /// <returns>New instance of an typed object.</returns>
        /// <see cref="IRegisteredObject"/>
        object GetInstance([NotNull] IRegisteredObject registeredObject, bool isExternal = false);
    }
}

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
using JetBrains.Annotations;

namespace microDI
{
    /// <summary>
    /// Interface of IoC container.
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// Registers interface and it's corresponding implementation with life cycle of
        /// an instance itself.
        /// </summary>
        /// <typeparam name="TInterface">Type of interface</typeparam>
        /// <typeparam name="TImplementation">Type of implementation</typeparam>
        /// <param name="lifeCycle">Life cycle of registered type.</param>
        /// <see cref="IReferencedObject"/>
        /// <see cref="ILifeCyclePolicy"/>
        [NotNull] IReferencedObject RegisterAs<TInterface, TImplementation>([NotNull] ILifeCyclePolicy lifeCycle)
            where TImplementation : TInterface;

        /// <summary>
        /// Registers singleton instance.
        /// </summary>
        /// <typeparam name="TInterface">Type of interface</typeparam>
        /// <typeparam name="TImplementation">Type of implementation</typeparam>
        /// <param name="instanceCreatorFunc">Function to be used for instantiating.</param>
        /// <param name="lifeCycle">Life cycle of registered type.</param>
        /// <see cref="IReferencedObject"/>
        /// <see cref="ILifeCyclePolicy"/>
        [NotNull] IReferencedObject RegisterAs<TInterface, TImplementation>(
            [NotNull] Func<IContainer, TImplementation> instanceCreatorFunc,
            [NotNull] ILifeCyclePolicy lifeCycle)
            where TImplementation : TInterface;

        /// <summary>
        /// Register class and creator function to be used for instantiation with life
        /// cycle.
        /// </summary>
        /// <typeparam name="TClass">Type of class to be registered</typeparam>
        /// <param name="instanceCreatorFunc">Function to be used for instantiating</param>
        /// <param name="lifeCycle">Life cycle of registered type.</param>
        /// <see cref="IReferencedObject"/>
        /// <see cref="ILifeCyclePolicy"/>
        [NotNull] IReferencedObject Register<TClass>(
            [NotNull] Func<IContainer, TClass> instanceCreatorFunc,
            [NotNull] ILifeCyclePolicy lifeCycle);

        /// <summary>
        /// Resolves interface with corresponding class implementation.
        /// </summary>
        /// <param name="resolveType">Concrete .NET type of interface</param>
        /// <returns>Instance of an specified object.</returns>
        [NotNull] object Resolve([NotNull] Type resolveType);

        /// <summary>
        /// Resolves interface with corresponding class implementation.
        /// </summary>
        /// <typeparam name="TInterface">Type of interface</typeparam>
        /// <returns>Instance of an specified object.</returns>
        [NotNull] TInterface Resolve<TInterface>();

        /// <summary>
        /// Get referenced object from registry service to operate with registered
        /// type and change or modify it's representation in IoC container.
        /// </summary>
        /// <typeparam name="TClass">Type of registered class or interface</typeparam>
        /// <returns>Description of registered object in container.</returns>
        /// <see cref="IReferencedObject"/>
        [NotNull] IReferencedObject GetReferencedType<TClass>();
    }
}

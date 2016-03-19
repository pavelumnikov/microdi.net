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
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using microDI.Exceptions;
using microDI.Internal.Assert;
using microDI.Internal.Extensions;

namespace microDI.Internal.Services
{
    internal class ActivationService : IActivationService
    {
        private readonly IContainer _container;

        public ActivationService([NotNull] IContainer container)
        {
            _container = RuntimeCheck.NotNull(container, nameof(container));
        }

        public object GetInstance(IRegisteredObject registeredObject, bool isExternal = false)
        {
            var internalRegisteredObject = RuntimeCheck.NotNull(registeredObject, nameof(registeredObject)).AsInternal();

            object result;
            var type = registeredObject.Type;

            try
            {
                if (internalRegisteredObject.IsInternal && isExternal)
                    throw new OnlyForInternalUseException(type);

                result = internalRegisteredObject.HasCustomizedCreatorFunction
                    ? internalRegisteredObject.OverridenInstanceCreatorFunction(_container)
                    : DefaultInstanceCreator(_container, type);
            }
            catch (Exception e)
            {
                throw new ActivationException($"Failed to activate object with type:[{type.Name}]", e);
            }

            return result;
        }

        private object DefaultInstanceCreator([NotNull] IContainer container, [NotNull] Type type)
        {
            RuntimeCheck.NotNull(type, nameof(type));

            var constructor = type.GetTypeInfo().DeclaredConstructors.Single();
            var arguments = constructor.GetParameters().Select(p => container.Resolve(p.ParameterType)).ToArray();

            return Activator.CreateInstance(type, arguments);
        }
    }
}

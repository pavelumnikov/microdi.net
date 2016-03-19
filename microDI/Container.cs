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
using microDI.Internal;
using microDI.Internal.Assert;
using microDI.Internal.Services;

namespace microDI
{
    public sealed class Container : IContainer
    {
        private readonly ActivationService _activationService;
        private readonly ObjectRegistryService _registryService;

        public Container()
        {
            _registryService = new ObjectRegistryService();
            _activationService = new ActivationService(this);
        }

        public IReferencedObject RegisterAs<TInterface, TImplementation>(ILifeCyclePolicy lifeCycle)
            where TImplementation : TInterface
        {
            RuntimeCheck.NotNull(lifeCycle, nameof(lifeCycle));

            return _registryService.Register(
                typeof(TInterface), new RegisteredObject(null, typeof(TImplementation), lifeCycle));
        }

        public IReferencedObject RegisterAs<TInterface, TImplementation>(
            Func<IContainer, TImplementation> instanceCreatorFunc,
            ILifeCyclePolicy lifeCycle)
            where TImplementation : TInterface
        {
            RuntimeCheck.NotNull(instanceCreatorFunc, nameof(instanceCreatorFunc));
            RuntimeCheck.NotNull(lifeCycle, nameof(lifeCycle));

            return _registryService.Register(
                typeof(TInterface), new RegisteredObject(
                    c => instanceCreatorFunc(c), typeof(TImplementation), lifeCycle));
        }

        public IReferencedObject Register<TClass>(
            Func<IContainer, TClass> instanceCreatorFunc, ILifeCyclePolicy lifeCycle)
        {
            RuntimeCheck.NotNull(instanceCreatorFunc, nameof(instanceCreatorFunc));
            RuntimeCheck.NotNull(lifeCycle, nameof(lifeCycle));

            var registeringType = typeof(TClass);

            return _registryService.Register(
                registeringType, new RegisteredObject(c => instanceCreatorFunc(c), registeringType, lifeCycle));
        }

        public object Resolve(Type resolveType)
        {
            return _activationService.GetInstance(_registryService.GetRegisteredObject(resolveType), true);
        }

        public TInterface Resolve<TInterface>()
        {
            return (TInterface)Resolve(typeof(TInterface));
        }

        public IReferencedObject GetReferencedType<TClass>()
        {
            return new ReferencedObject(_registryService.GetRegisteredObject(typeof(TClass)), _registryService);
        }
    }
}

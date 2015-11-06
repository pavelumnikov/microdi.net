using System;
using System.Collections.Generic;
using microDI.Exceptions;
using microDI.Internal;
using microDI.Internal.Assert;

namespace microDI
{
    public class Container : IContainer
    {
        private readonly ActivationFactory _activationFactory;
        private readonly Dictionary<Type, RegisteredType> _typeRegistery;

        public Container()
        {
            _activationFactory = new ActivationFactory(this);
            _typeRegistery = new Dictionary<Type, RegisteredType>();
        }

        public void RegisterAs<TInterface, TImplementation>(ILifeCyclePolicy lifeCycle)
            where TImplementation : TInterface
        {
            Assertions.NotNull(lifeCycle, "lifeCycle");

            var registeringType = typeof (TInterface);

            if (_typeRegistery.ContainsKey(registeringType))
                throw new TypeAlreadyRegisteredException(registeringType.Name);

            _typeRegistery.Add(registeringType, 
                new RegisteredType(null, typeof (TImplementation), lifeCycle));
        }

        public void RegisterAs<TInterface, TImplementation>(
            Func<IContainer, TImplementation> instanceCreatorFunc,
            ILifeCyclePolicy lifeCycle)
            where TImplementation : TInterface
        {
            var interfaceType = typeof(TInterface);

            if (_typeRegistery.ContainsKey(interfaceType))
                throw new TypeAlreadyRegisteredException(interfaceType.Name);

            _typeRegistery.Add(interfaceType,
                new RegisteredType(c => instanceCreatorFunc(c), typeof(TImplementation), lifeCycle));
        }

        public void Register<TClass>(Func<IContainer, TClass> instanceCreatorFunc, ILifeCyclePolicy lifeCycle)
        {
            Assertions.NotNull(instanceCreatorFunc, "instanceCreatorFunc");
            Assertions.NotNull(lifeCycle, "lifeCycle");

            var registeringType = typeof(TClass);

            if (_typeRegistery.ContainsKey(registeringType))
                throw new TypeAlreadyRegisteredException(registeringType.Name);

            _typeRegistery.Add(registeringType,
                new RegisteredType(c => instanceCreatorFunc(c), registeringType, lifeCycle));
        }

        public object Resolve(Type resolveType)
        {
            return GetInstance(resolveType);
        }

        public TInterface Resolve<TInterface>()
        {
            return (TInterface)Resolve(typeof(TInterface));
        }

        private object GetInstance(Type interfaceType)
        {
            RegisteredType registeredType;

            if (!_typeRegistery.TryGetValue(interfaceType, out registeredType))
                throw new TypeNotRegisteredException(interfaceType.Name);

            return registeredType.LifeCycle.Get(_activationFactory, registeredType);
        }
    }
}

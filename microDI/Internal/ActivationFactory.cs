using System;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using microDI.Exceptions;
using microDI.Internal.Assert;

namespace microDI.Internal
{
    internal class ActivationFactory : IActivationFactory
    {
        private readonly IContainer _container;

        public ActivationFactory([NotNull] IContainer container)
        {
            Assertions.NotNull(container, "container");

            _container = container;
        }

        public object GetInstance([NotNull] RegisteredType registeredType)
        {
            Assertions.NotNull(registeredType, "registeredType");

            object result;
            var type = registeredType.Type;

            try
            {
                result = registeredType.HasCustomizedCreatorFunction
                    ? registeredType.OverridenInstanceCreatorFunction(_container)
                    : DefaultInstanceCreator(type);
            }
            catch (Exception e)
            {
                var error = string.Format("Failed to activate object with type:[{0}]", type.Name);
                throw new ActivationException(error, e);
            }

            return result;
        }

        private object DefaultInstanceCreator([NotNull] Type type)
        {
            Assertions.NotNull(type, "type");

            var constructor = type.GetTypeInfo().DeclaredConstructors.Single();
            var arguments = constructor.GetParameters().Select(p => _container.Resolve(p.ParameterType)).ToArray();
            return Activator.CreateInstance(type, arguments);
        }
    }
}

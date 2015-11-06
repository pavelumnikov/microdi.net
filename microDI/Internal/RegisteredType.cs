using System;
using JetBrains.Annotations;

namespace microDI.Internal
{
    public class RegisteredType
    {
        public Type Type { get; }

        internal  ILifeCyclePolicy LifeCycle { get; }
        internal Func<IContainer, object> OverridenInstanceCreatorFunction { get; }
        internal bool HasCustomizedCreatorFunction
        {
            get { return OverridenInstanceCreatorFunction != null; }
        }

        public RegisteredType(
            [CanBeNull] Func<IContainer, object> createInstanceFunc,
            [NotNull] Type registedType,
            [NotNull] ILifeCyclePolicy lifeCycle)
        {
            OverridenInstanceCreatorFunction = createInstanceFunc;
            Type = registedType;
            LifeCycle = lifeCycle;
        }
    }
}

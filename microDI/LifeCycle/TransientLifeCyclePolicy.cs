using microDI.Internal;

namespace microDI.LifeCycle
{
    public class TransientLifeCyclePolicy : ILifeCyclePolicy
    {
        public object Get(IActivationFactory activationFactory, RegisteredType registeredType)
        {
            return activationFactory.GetInstance(registeredType);
        }
    }
}

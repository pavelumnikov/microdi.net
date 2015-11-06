using microDI.Internal;

namespace microDI.LifeCycle
{
    public class SingletonLifeCyclePolicy : ILifeCyclePolicy
    {
        private object _value;

        public object Get(IActivationFactory activationFactory, RegisteredType registeredType)
        {
            object result;

            if (_value != null)
                result = _value;
            else
                result = _value = activationFactory.GetInstance(registeredType);

            return result;
        }
    }
}

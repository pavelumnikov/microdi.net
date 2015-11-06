using System.Threading;
using microDI.Internal;

namespace microDI.LifeCycle
{
    public class ThreadLocalLifeCyclePolicy : ILifeCyclePolicy
    {
        private readonly ThreadLocal<object> _threadLocalValue = new ThreadLocal<object>(); 

        public object Get(IActivationFactory activationFactory, RegisteredType registeredType)
        {
            object result;

            if (_threadLocalValue.Value != null)
                result = _threadLocalValue.Value;
            else
                result = _threadLocalValue.Value = activationFactory.GetInstance(registeredType);

            return result;
        }
    }
}

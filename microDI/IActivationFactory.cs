using JetBrains.Annotations;
using microDI.Internal;

namespace microDI
{
    /// <summary>
    /// <c>ActivationFactory</c> is an interface for instance creator. It doesn't depend on
    /// any life cycle policy, it just creates new instance of an <c>object</c> by known type.
    ///  </summary>
    public interface IActivationFactory
    {
        /// <summary>
        /// Creates new instance of type from registered type in IoC.
        /// </summary>
        /// <param name="registeredType">Instance of sidecar for registered type.</param>
        /// <returns>New instance of an typed object.</returns>
        object GetInstance([NotNull] RegisteredType registeredType);
    }
}

using JetBrains.Annotations;
using microDI.Internal;

namespace microDI
{
    /// <summary>
    /// Basic interface for every policy to be created.
    /// </summary>
    public interface ILifeCyclePolicy
    {
        /// <summary>
        /// Get value from policy instance.
        /// </summary>
        /// <returns>Got value from policy instance.</returns>
        [NotNull] object Get(
            [NotNull] IActivationFactory activationFactory, 
            [NotNull] RegisteredType registeredType);
    }
}

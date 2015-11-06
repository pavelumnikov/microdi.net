using System;
using JetBrains.Annotations;

namespace microDI
{
    /// <summary>
    /// Interface of IoC constainer.
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// Registers interface and it's corresponding implementation with life cycle of
        /// an instance itself.
        /// </summary>
        /// <typeparam name="TInterface">Type of interface</typeparam>
        /// <typeparam name="TImplementation">Type of implementation</typeparam>
        /// <param name="lifeCycle">Life cycle of registered type.</param>
        void RegisterAs<TInterface, TImplementation>([NotNull] ILifeCyclePolicy lifeCycle)
            where TImplementation : TInterface;

        /// <summary>
        /// Registers singleton instance.
        /// </summary>
        /// <typeparam name="TInterface">Type of interface</typeparam>
        /// <typeparam name="TImplementation">Type of implementation</typeparam>
        /// <param name="instanceCreatorFunc">Function to be used for instantiating.</param>
        /// <param name="lifeCycle">Life cycle of registered type.</param>
        void RegisterAs<TInterface, TImplementation>(
            [NotNull] Func<IContainer, TImplementation> instanceCreatorFunc,
            [NotNull] ILifeCyclePolicy lifeCycle)
            where TImplementation : TInterface;

        /// <summary>
        /// Register class and creator function to be used for instantiation with life
        /// cycle.
        /// </summary>
        /// <typeparam name="TClass">Type of class to be registered</typeparam>
        /// <param name="instanceCreatorFunc">Function to be used for instantiating</param>
        /// <param name="lifeCycle">Life cycle of registered type.</param>
        void Register<TClass>(
            [NotNull] Func<IContainer, TClass> instanceCreatorFunc,
            [NotNull] ILifeCyclePolicy lifeCycle);

        /// <summary>
        /// Resolves interface with corresponding class implementation.
        /// </summary>
        /// <param name="resolveType">Concrete .NET type of interface</param>
        /// <returns>Instance of an specified object.</returns>
        [NotNull] object Resolve([NotNull] Type resolveType);

        /// <summary>
        /// Resolves interface with corresponding class implementation.
        /// </summary>
        /// <typeparam name="TInterface">Type of interface</typeparam>
        /// <returns>Instance of an specified object.</returns>
        [NotNull] TInterface Resolve<TInterface>();
    }
}

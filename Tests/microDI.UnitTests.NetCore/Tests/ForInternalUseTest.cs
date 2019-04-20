using System;
using FluentAssertions;
using microDI.Exceptions;
using microDI.LifeCycle;
using microDI.UnitTests.NetCore.Configurations.SimpleClassAndInterface;
using microDI.UnitTests.NetCore.Configurations.SimpleClassAndTwoInterfaces;
using microDI.UnitTests.NetCore.Configurations.TwoSimpleClassesAndTwoInterfaces;
using Xunit;

namespace microDI.UnitTests.NetCore.Tests
{
    public class ForInternalUseTest
    {
        [Fact]
        public void RegisterAndResolve_SimpleInterface_OnlyForInternalUseException()
        {
            var container = new Container();

            Action registerInterfaceAction = 
                () => container.RegisterAs<ISimpleInterface, SimpleClassImplementsISimpleInterface>(
                    new TransientLifeCyclePolicy()).ForInternalUse();

            Action resolveInternalInterfaceAction = 
                () => container.Resolve<ISimpleInterface>();

            registerInterfaceAction.Should().NotThrow();
            resolveInternalInterfaceAction.Should().Throw<ActivationException>()
                .WithInnerException<OnlyForInternalUseException>();
        }

        [Fact]
        public void RegisterAndResolve_WithAutoWire_TwoInterfacesWithImplementor_OneOnlyForInternalUse()
        {
            var container = new Container();

            Action registerInterfaceAction = 
                () => container.RegisterAs<ISecondInterface, SimpleClassImplementsTwoInterfaces>(
                    new SingletonLifeCyclePolicy()).AutoWire<IFirstInterface>().ForInternalUse();

            Action resolveInterfaceAction = 
                () => container.Resolve<ISecondInterface>();

            registerInterfaceAction.Should().NotThrow();
            resolveInterfaceAction.Should().Throw<ActivationException>()
                .WithInnerException<OnlyForInternalUseException>();
        }

        [Fact]
        public void RegisterAndResolve_ClassDependencyOnInterface_DependencyOnlyForInternalUse()
        {
            var container = new Container();

            Action registerFirstInterfaceAction =
                () => container.RegisterAs<IFirstIndependentInterface, ImplementorOfFirstInterface>(
                    new SingletonLifeCyclePolicy()).ForInternalUse();

            Action registerSecondInterfaceAction =
                () => container.RegisterAs<ISecondIndependentInterface, ImplementorOfSecondInterface>(
                    new SingletonLifeCyclePolicy());

            Action resolveFirstInterfaceAction =
                () => container.Resolve<IFirstIndependentInterface>();

            Action resolveSecondInterfaceAction =
                () => container.Resolve<ISecondIndependentInterface>();

            registerFirstInterfaceAction.Should().NotThrow();
            registerSecondInterfaceAction.Should().NotThrow();

            resolveFirstInterfaceAction.Should().Throw<ActivationException>()
                .WithInnerException<OnlyForInternalUseException>();

            resolveSecondInterfaceAction.Should().NotThrow<ActivationException>();
        }
    }
}

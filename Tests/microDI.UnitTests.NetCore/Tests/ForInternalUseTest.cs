using System;
using FluentAssertions;
using microDI.Exceptions;
using microDI.LifeCycle;
using microDI.UnitTests.NetCore.Configurations.SimpleClassAndInterface;
using microDI.UnitTests.NetCore.Configurations.SimpleClassAndTwoInterfaces;
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
    }
}

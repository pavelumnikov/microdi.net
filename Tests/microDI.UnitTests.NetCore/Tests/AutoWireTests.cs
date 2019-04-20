using System;
using FluentAssertions;
using microDI.LifeCycle;
using microDI.UnitTests.NetCore.Configurations.SimpleClassAndTwoInterfaces;
using Xunit;

namespace microDI.UnitTests.NetCore.Tests
{
    public class AutoWireTests
    {
        [Fact]
        public void Register_SimpleInterface_AutoWireWithRegistered_Test()
        {
            var container = new Container();

            Action registerInterfacesAction = 
                () => container.RegisterAs<IFirstInterface, SimpleClassImplementsTwoInterfaces>(
                new TransientLifeCyclePolicy()).AutoWire<ISecondInterface>();

            registerInterfacesAction.Should().NotThrow();
        }

        [Fact]
        public void RegisterAndResolve_WithAutoWire_TwoInterfacesWithImplementor_AutoWireWithRegistered()
        {
            var container = new Container();

            Action registerInterfacesAction = () => container.RegisterAs<IFirstInterface, SimpleClassImplementsTwoInterfaces>(
                new TransientLifeCyclePolicy()).AutoWire<ISecondInterface>();

            Action resolveFirstInterfaceAction = (() => container.Resolve<IFirstInterface>());
            Action resolveSecondInterfaceAction = (() => container.Resolve<ISecondInterface>());

            registerInterfacesAction.Should().NotThrow();
            resolveFirstInterfaceAction.Should().NotThrow();
            resolveSecondInterfaceAction.Should().NotThrow();
        }
    }
}

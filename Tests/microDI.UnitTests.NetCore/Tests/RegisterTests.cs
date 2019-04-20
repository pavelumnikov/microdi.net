using System;
using FluentAssertions;
using microDI.Exceptions;
using microDI.LifeCycle;
using microDI.UnitTests.NetCore.Configurations.SimpleClassAndInterface;
using Xunit;

namespace microDI.UnitTests.NetCore.Tests
{
    public class RegisterTests
    {
        [Fact]
        public void Register_SimpleInterfaceWithImplementorClass_NoThrow()
        {
            var container = new Container();

            Action registerInterfacesAction = 
                () => container.RegisterAs<ISimpleInterface, SimpleClassImplementsISimpleInterface>(
                    new TransientLifeCyclePolicy());

            registerInterfacesAction.Should().NotThrow();
        }

        [Fact]
        public void Register_SimpleInterfaceWithImplementorClass_ThrowsAlreadyRegisteredException()
        {
            var container = new Container();

            Action registerInterfacesAction = 
                () => container.RegisterAs<ISimpleInterface, SimpleClassImplementsISimpleInterface>(
                    new TransientLifeCyclePolicy());

            registerInterfacesAction.Should().NotThrow();
            // try to execute one more time, and at this time we must fail
            registerInterfacesAction.Should().Throw<TypeAlreadyRegisteredException>();
        }
    }
}

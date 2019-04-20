using System;
using FluentAssertions;
using microDI.Exceptions;
using microDI.LifeCycle;
using microDI.UnitTests.NetCore.Configurations.SimpleClassAndInterface;
using Xunit;

namespace microDI.UnitTests.NetCore.Tests
{
    public class ResolveTests
    {
        [Fact]
        public void Resolve_SimpleInterface_NoThrow()
        {
            var container = new Container();

            Action registerInterfacesAction = 
                () => container.RegisterAs<ISimpleInterface, SimpleClassImplementsISimpleInterface>(
                    new TransientLifeCyclePolicy());

            Action resolveInterfaceAction =
                () => container.Resolve<ISimpleInterface>();

            registerInterfacesAction.Should().NotThrow();
            resolveInterfaceAction.Should().NotThrow();
        }

        [Fact]
        public void Resolve_SimpleInterface_ExplicitCreation_NoThrow()
        {
            var container = new Container();

            Action registerInterfacesAction = 
                () => container.RegisterAs<ISimpleInterface, SimpleClassImplementsISimpleInterface>(
                    c => new SimpleClassImplementsISimpleInterface(), new TransientLifeCyclePolicy());

            Action resolveInterfaceAction = 
                () => container.Resolve<ISimpleInterface>();

            registerInterfacesAction.Should().NotThrow();
            resolveInterfaceAction.Should().NotThrow();
        }

        [Fact]
        public void Resolve_SimpleInterface_ThrowsNotRegisteredException()
        {
            var container = new Container();

            Action resolveInterfaceAction =
                () => container.Resolve<ISimpleInterface>();

            resolveInterfaceAction.Should().Throw<TypeNotRegisteredException>();
        }
    }
}
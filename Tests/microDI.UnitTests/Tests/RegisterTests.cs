using microDI.Exceptions;
using microDI.LifeCycle;
using microDI.UnitTests.Configurations.SimpleClassAndInterface;
using NUnit.Framework;

namespace microDI.UnitTests.Tests
{
    [TestFixture]
    public class RegisterTests
    {
        [Test]
        public void Register_SimpleInterfaceWithImplementorClass_NoThrow()
        {
            var container = new Container();

            Assert.DoesNotThrow(() => container.RegisterAs<ISimpleInterface, SimpleClassImplementsISimpleInterface>(
                new TransientLifeCyclePolicy()));
        }

        [Test]
        public void Register_SimpleInterfaceWithImplementorClass_ThrowsAlreadyRegisteredException()
        {
            var container = new Container();

            Assert.DoesNotThrow(() => container.RegisterAs<ISimpleInterface, SimpleClassImplementsISimpleInterface>(
                new TransientLifeCyclePolicy()));

            Assert.Throws<TypeAlreadyRegisteredException>(
                () => container.RegisterAs<ISimpleInterface, SimpleClassImplementsISimpleInterface>(
                    new TransientLifeCyclePolicy()));
        }
    }
}

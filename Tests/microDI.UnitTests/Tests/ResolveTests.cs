using microDI.Exceptions;
using microDI.LifeCycle;
using microDI.UnitTests.Configurations.SimpleClassAndInterface;
using NUnit.Framework;

namespace microDI.UnitTests.Tests
{
    [TestFixture]
    public class ResolveTests
    {
        [Test]
        public void Resolve_SimpleInterface_NoThrow()
        {
            var container = new Container();

            Assert.DoesNotThrow(() => container.RegisterAs<ISimpleInterface, SimpleClassImplementsISimpleInterface>(
                new TransientLifeCyclePolicy()));

            Assert.DoesNotThrow(() => container.Resolve<ISimpleInterface>());
        }

        [Test]
        public void Resolve_SimpleInterface_ExplicitCreation_NoThrow()
        {
            var container = new Container();

            Assert.DoesNotThrow(() => container.RegisterAs<ISimpleInterface, SimpleClassImplementsISimpleInterface>(
                c => new SimpleClassImplementsISimpleInterface(), new TransientLifeCyclePolicy()));

            Assert.DoesNotThrow(() => container.Resolve<ISimpleInterface>());
        }

        [Test]
        public void Resolve_SimpleInterface_ThrowsNotRegisteredException()
        {
            var container = new Container();
            Assert.Throws<TypeNotRegisteredException>(() => container.Resolve<ISimpleInterface>());
        }
    }
}
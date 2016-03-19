using microDI.Exceptions;
using microDI.LifeCycle;
using microDI.UnitTests.Configurations.SimpleClassAndInterface;
using microDI.UnitTests.Configurations.SimpleClassAndTwoInterfaces;
using NUnit.Framework;

namespace microDI.UnitTests.Tests
{
    [TestFixture]
    public class ForInternalUseTest
    {
        [Test]
        public void RegisterAndResolve_SimpleInterface_OnlyForInternalUseException()
        {
            var container = new Container();

            Assert.DoesNotThrow(() => container.RegisterAs<ISimpleInterface, SimpleClassImplementsISimpleInterface>(
                new TransientLifeCyclePolicy()).ForInternalUse());

            Assert.That(() => container.Resolve<ISimpleInterface>(),
                Throws.InnerException.With.TypeOf<OnlyForInternalUseException>());
        }

        [Test]
        public void RegisterAndResolve_WithAutoWire_TwoInterfacesWithImplementor_OneOnlyForInternalUse()
        {
            var container = new Container();

            Assert.DoesNotThrow(() => container.RegisterAs<IFirstInterface, SimpleClassImplementsTwoInterfaces>(
                new SingletonLifeCyclePolicy()).AutoWire<ISecondInterface>().ForInternalUse());

            Assert.DoesNotThrow(() => container.Resolve<ISecondInterface>());

            Assert.That(() => container.Resolve<IFirstInterface>(),
                Throws.InnerException.With.TypeOf<OnlyForInternalUseException>());
        }
    }
}

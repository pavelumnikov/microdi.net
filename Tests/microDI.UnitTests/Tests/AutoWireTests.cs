using microDI.LifeCycle;
using microDI.UnitTests.Configurations.SimpleClassAndTwoInterfaces;
using NUnit.Framework;

namespace microDI.UnitTests.Tests
{
    [TestFixture]
    public class AutoWireTests
    {
        [Test]
        public void Register_SimpleInterface_AutoWireWithRegistered_Test()
        {
            var container = new Container();

            Assert.DoesNotThrow(() => container.RegisterAs<IFirstInterface, SimpleClassImplementsTwoInterfaces>(
                new TransientLifeCyclePolicy()).AutoWire<ISecondInterface>());
        }

        [Test]
        public void RegisterAndResolve_WithAutoWire_TwoInterfacesWithImplementor_AutoWireWithRegistered()
        {
            var container = new Container();

            Assert.DoesNotThrow(() => container.RegisterAs<IFirstInterface, SimpleClassImplementsTwoInterfaces>(
                new TransientLifeCyclePolicy()).AutoWire<ISecondInterface>());

            Assert.DoesNotThrow(() => container.Resolve<IFirstInterface>());

            Assert.DoesNotThrow(() => container.Resolve<ISecondInterface>());
        }
    }
}

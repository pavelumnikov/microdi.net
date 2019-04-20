using System;

namespace microDI.UnitTests.NetCore.Configurations.TwoSimpleClassesAndTwoInterfaces
{
    class ImplementorOfSecondInterface : ISecondIndependentInterface
    {
        public ImplementorOfSecondInterface(IFirstIndependentInterface firstInterface)
        {
            if(firstInterface == null)
                throw new ArgumentNullException(nameof(firstInterface));
        }
    }
}

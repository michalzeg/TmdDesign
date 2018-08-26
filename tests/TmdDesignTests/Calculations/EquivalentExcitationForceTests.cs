using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using TmdDesign.Calculations.Calculations;

namespace TmdDesignTests.Calculations
{
    [TestFixture]
    public class EquivalentExcitationForceTests
    {
        [Test]
        public void CalculateEquivalenDynamicForceTest()
        {
            double actualEquivalentDynamicForce = 41052.677;

            var inputData = new ExcitationForceInputData
            {
                ModalMass = 211299.94,
                NaturalFrequency = 1.3685721,
                ExcitationFrequency = 1.3685721,
                DampingRatio = 0.002,
                DynamicDisplacement = 0.041544832
            };

            var equivalentForceCalcs = new EquivalentExcitationForce();
            equivalentForceCalcs.CalculateEquivalenDynamicForce(inputData);
            double expectedEquivalentDynamicForce = Math.Round(equivalentForceCalcs.DynamicForce, 3);

            Assert.AreEqual(expectedEquivalentDynamicForce, actualEquivalentDynamicForce);
        }
    }
}
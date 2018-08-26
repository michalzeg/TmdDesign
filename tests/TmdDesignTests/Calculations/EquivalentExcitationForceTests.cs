using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdDesign.ExcitationForces;
using TmdDesign.SimpleClasses;
using NUnit.Framework;

namespace TmdDesign.ExcitationForces.Tests
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

            var eqForceCalcs = new EquivalentExcitationForce();
            double expectedEquivalentDynamicForce = Math.Round(eqForceCalcs.CalculateEquivalenDynamicForce(inputData), 3);

            Assert.AreEqual(expectedEquivalentDynamicForce, actualEquivalentDynamicForce);
        }
    }
}
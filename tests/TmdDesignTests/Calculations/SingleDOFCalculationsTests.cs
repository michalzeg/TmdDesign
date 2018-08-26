using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdDesign.Calculations;
using TmdDesign.Calculations.Calculations;
using TmdDesign.Calculations.Parameters;

namespace TmdDesignTests.Calculations
{
    [TestFixture]
    public class SingleDOFCalculationsTests
    {
        private StructureParameters structParms;
        private ForceParameters forceParms;

        [OneTimeSetUp]
        public void Setup()
        {
            this.structParms = new StructureParameters(10000, 1.8006326323142123, 0.003, false);
            this.forceParms = new ForceParameters();
            forceParms.ForceValue = 1000000;
        }

        [Test]
        public void SingleDOFCalculations_ReturnsPropertDOF()
        {
            double expectedU = 10.089;
            double expectedA = 1290.488;

            var calcSDF = new SingleDOFCalculations(this.structParms, this.forceParms);

            var omega = 1.8;

            var resuls = calcSDF.Calculate(omega);

            var actualU = resuls.StructureDisplacement;
            var actualA = resuls.StructureAcceleration;

            Assert.AreEqual(actualU, expectedU, 0.1);
            Assert.AreEqual(actualA, expectedA, 0.1);
        }
    }
}
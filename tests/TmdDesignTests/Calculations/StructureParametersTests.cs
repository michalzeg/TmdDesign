using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdDesign.Calculations;
using TmdDesign.Calculations.Parameters;

namespace TmdDesignTests.Calculations
{
    [TestFixture]
    public class StructureParametersTests
    {
        private double mass = 6000;
        private double omega = 10;
        private double ksi = 0.05;

        [Test]
        public void StructureParametersIgnoreDamping()
        {
            var strucParm = new StructureParameters(this.mass, this.omega, this.ksi, true);

            List<double> expected = new List<double>() { 6000, 23687050.563, 10, 0.05, 0 };

            List<double> actual = new List<double>
            {
                strucParm.Mass,
                Math.Round(strucParm.Stiffness, 3),
                strucParm.NaturalFrequency,
                strucParm.Ksi,
                strucParm.Damping
            };

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void StructureParametersDontIgnoreDamping()
        {
            var strucParm = new StructureParameters(this.mass, this.omega, this.ksi, false);

            List<double> expected = new List<double>() { 6000, 23687050.563, 10, 0.05, 37699.112 };

            List<double> actual = new List<double>
            {
                strucParm.Mass,
                Math.Round(strucParm.Stiffness, 3),
                strucParm.NaturalFrequency,
                strucParm.Ksi,
                Math.Round(strucParm.Damping, 3)
            };

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
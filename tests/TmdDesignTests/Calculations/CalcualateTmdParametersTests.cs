using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdDesign.Calculations;

using TmdDesign.SimpleClasses;

namespace TmdDesign.Calculations.Tests
{
    [TestFixture]
    public class CalcualateTmdParametersTests
    {
        [Test]
        public void CalculateParameters_ReturnsPropertParameters()
        {
            var strParm = new StructureParameters(6000, 10, 0.05, false);

            List<double> expected = new List<double>() { 300, 1074242.656, 4569.393, 0.05, 9.524, 0.952, 0.127 };
            var tmdCalcs = new TmdParametersCalculations();
            var tmdParm = tmdCalcs.CalculateAllParameters(strParm, 0.05);

            var actual = new List<double>
            {
                Math.Round(tmdParm.Mass, 3),
                Math.Round(tmdParm.Stiffness, 3),
                Math.Round(tmdParm.Damping, 3),
                Math.Round(tmdParm.Mi, 3),
                Math.Round(tmdParm.OmegaD, 3),
                Math.Round(tmdParm.DeltaOpt, 3),
                Math.Round(tmdParm.Ksi, 3)
            };
            CollectionAssert.AreEqual(actual, expected);
        }
    }
}
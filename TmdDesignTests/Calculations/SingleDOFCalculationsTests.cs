using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdDesign.Calculations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TmdDesign.SimpleClasses;
namespace TmdDesign.Calculations.Tests
{
    [TestClass()]
    public class SingleDOFCalculationsTests
    {

        private StructureParameters structParms;
        private ForceParameters forceParms;
        public SingleDOFCalculationsTests()
        {
            this.structParms = new StructureParameters(10000, 1.8006326323142123, 0.003, false);
            this.forceParms = new ForceParameters();
            forceParms.ForceValue = 1000000;
        }

        [TestMethod()]
        public void SingleDOFTest()
        {
            double u_actual = 10.089; //actual displacement
            double a_actual = 1290.488; //actual acceleration

            SingleDOFCalculations calcSDF = new SingleDOFCalculations(this.structParms, this.forceParms);

            double omega = 1.8; //excitation frequency

            ResultsSingleDOF res = calcSDF.Calculate(omega);

            double u_expected = Math.Round(res.StructureU, 3);
            double a_expected = Math.Round(res.StructureA, 3);

            Assert.AreEqual(u_expected, u_actual);
            Assert.AreEqual(a_expected, a_actual);
        }
    }
}

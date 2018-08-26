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
    public class CalcualateTmdParametersTests
    {
        
        
        [TestMethod()]
        public void CalculateParametersTest()
        {
            var strParm = new StructureParameters(6000, 10, 0.05, false);
            
            //mass, stiffness,damping, mass ratio, tmd frequency, optimal frequency ratio, critical damping ratio
            List<double> actual = new List<double>() { 300, 1074242.656, 4569.393, 0.05, 9.524, 0.952, 0.127 };
            var tmdCalcs = new TmdParametersCalculations();
            var tmdParm = tmdCalcs.CalculateAllParameters(strParm, 0.05);

            var expected = new List<double>();
            expected.Add(Math.Round(tmdParm.M,3));
            expected.Add(Math.Round(tmdParm.K,3));
            expected.Add(Math.Round(tmdParm.C,3));
            expected.Add(Math.Round(tmdParm.Mi,3));
            expected.Add(Math.Round(tmdParm.OmegaD,3));
            expected.Add(Math.Round(tmdParm.DeltaOpt,3));
            expected.Add(Math.Round(tmdParm.Ksi,3));
            CollectionAssert.AreEqual(expected,actual);
        }
        
    }
}

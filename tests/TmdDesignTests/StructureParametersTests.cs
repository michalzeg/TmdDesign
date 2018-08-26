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
    public class StructureParametersTests
    {
        double m = 6000;
        double omega = 10;
        double ksi = 0.05;

        [TestMethod()]
        public void StructureParametersIgnoreDamping()
        {

            var strucParm = new StructureParameters(this.m, this.omega, this.ksi, true);

            //mass,stiffness,frequency, critical damping ratio, damping
            List<double> actual = new List<double>() { 6000,23687050.563, 10, 0.05, 0 };
            

            List<double> expected = new List<double>();
            expected.Add(strucParm.M);
            expected.Add(Math.Round(strucParm.K, 3));
            expected.Add(strucParm.NaturalFrequency);
            expected.Add(strucParm.Ksi);
            expected.Add(strucParm.C);


            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void StructureParametersDontIgnoreDamping()
        {
            var strucParm = new StructureParameters(this.m, this.omega, this.ksi, false);

            //mass,stiffness,frequency, critical damping ratio, damping
            List<double> actual = new List<double>() { 6000, 23687050.563, 10, 0.05, 37699.112 };


            List<double> expected = new List<double>();
            expected.Add(strucParm.M);
            expected.Add(Math.Round(strucParm.K, 3));
            expected.Add(strucParm.NaturalFrequency);
            expected.Add(strucParm.Ksi);
            expected.Add(Math.Round(strucParm.C,3));


            CollectionAssert.AreEqual(expected, actual);
        }
       
    }
}

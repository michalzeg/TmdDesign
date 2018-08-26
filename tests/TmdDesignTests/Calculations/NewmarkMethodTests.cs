using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TmdDesign.Calculations;
using TmdDesign.Matrix;
using TmdDesign.SimpleClasses;
using NUnit.Framework;
using TmdDesign.ExcitationForces;
using System.Linq;

namespace TmdDesign.Calculations.Tests
{
    [TestFixture]
    public class NewmarkMethodTests
    {
        private StructureParameters strParms;
        private TmdParameters tmdParms;
        private TimeParameters timeParms;
        private ExcitationFunction exFunc = new ExcitationFunction(ExcitationForces.ExcitationFunctions.Sin);
        private double force = 30000000;

        [OneTimeSetUp]
        public void Setup()
        {
            //structure parameteres
            this.strParms = new StructureParameters(6000, 10, 0.01, false);

            //tmd parameters
            var tmdCalculations = new TmdParametersCalculations();
            this.tmdParms = tmdCalculations.CalculateAllParameters(this.strParms, 0.01);
            this.timeParms = new TimeParameters(0, 0.01);
        }

        [Test]
        public void RunNewmarkMethodWithStrDampingNotIgnored()
        {
            List<double> u1_expected = new List<double> { 0, 0.007, 0.06, 0.246, 0.671, 1.354 };
            List<double> u2_expected = new List<double> { 0, 0.061, 0.322, 0.842, 1.467, 1.88 };
            List<double> v1_expected = new List<double> { 0, 1.434, 9.105, 28.198, 56.631, 80.165 };
            List<double> v2_expected = new List<double> { 0, 12.11, 40.171, 63.78, 61.211, 21.391 };
            List<double> a1_expected = new List<double> { 0, 286.73, 1247.506, 2571.08, 3115.529, 1591.303 };
            List<double> a2_expected = new List<double> { 0, 2422.006, 3190.155, 1531.772, -2045.682, -5918.371 };

            double exFrequency = 9;
            NewmarkMethod newmarkMethod = new NewmarkMethod(this.strParms, this.tmdParms, this.force, this.timeParms, 10);
            newmarkMethod.Calculate(exFrequency);

            List<Vector> u = newmarkMethod.Displacement.ToList();
            List<Vector> v = newmarkMethod.Velocity.ToList();
            List<Vector> a = newmarkMethod.Acceleration.ToList();

            List<double> u1_actual = new List<double>();
            List<double> u2_actual = new List<double>();
            List<double> v1_actual = new List<double>();
            List<double> v2_actual = new List<double>();
            List<double> a1_actual = new List<double>();
            List<double> a2_actual = new List<double>();

            for (int i = 0; i <= 5; i++)
            {
                u1_actual.Add(Math.Round(u[i].A1, 3));
                u2_actual.Add(Math.Round(u[i].A2, 3));
                v1_actual.Add(Math.Round(v[i].A1, 3));
                v2_actual.Add(Math.Round(v[i].A2, 3));
                a1_actual.Add(Math.Round(a[i].A1, 3));
                a2_actual.Add(Math.Round(a[i].A2, 3));
            }
            CollectionAssert.AreEqual(u1_expected, u1_actual);
            CollectionAssert.AreEqual(u2_expected, u2_actual);
            CollectionAssert.AreEqual(v1_expected, v1_actual);
            CollectionAssert.AreEqual(v2_expected, v2_actual);
            CollectionAssert.AreEqual(a1_expected, a1_actual);
            CollectionAssert.AreEqual(a2_expected, a2_actual);
        }
    }

    [TestFixture]
    public class NewmarkMethodTestsCase2
    {
        private StructureParameters strParms;
        private TmdParameters tmdParms;
        private TimeParameters timeParms;
        private ExcitationFunction exFunc = new ExcitationFunction(ExcitationForces.ExcitationFunctions.Sin);
        private double force = 30000000;

        [OneTimeSetUp]
        public void Setup()
        {
            this.strParms = new StructureParameters(6000, 10, 0.1, false);

            var tmdCalc = new TmdParametersCalculations();
            this.tmdParms = tmdCalc.CalculateAllParameters(this.strParms, 0.01);
            this.timeParms = new TimeParameters(0, 0.00001);
        }

        [Test]
        public void RunNewmarkMethodTestStrDampingNotIgnored2()
        {
            double exFrequency = 3;
            NewmarkMethod newmarkMethod = new NewmarkMethod(this.strParms, this.tmdParms, this.force, this.timeParms, 0.001);
            ResultsTMD nr = newmarkMethod.Calculate(exFrequency);

            double tmdA_actual = 544;
            double tmdU_actual = 1531;
            double strA_actual = 494;
            double strU_actual = 1390;

            int precision = 0;
            double tmdA_expected = Math.Round(nr.TmdAcceleration, precision);
            double tmdU_expected = Math.Round(nr.TmdDisplacement * 1000, precision);
            double strA_expected = Math.Round(nr.StructureAcceleration, precision);
            double strU_expected = Math.Round(nr.StructureDisplacement * 1000, precision);

            Assert.AreEqual(tmdA_expected, tmdA_actual);
            Assert.AreEqual(tmdU_expected, tmdU_actual);
            Assert.AreEqual(strA_expected, strA_actual);
            Assert.AreEqual(strU_expected, strU_actual);
        }
    }
}
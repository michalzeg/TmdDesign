using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TmdDesign.Calculations;
using TmdDesign.Matrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TmdDesign.SimpleClasses;
namespace TmdDesign.Calculations.Tests
{
    [TestClass()]
    public class NewmarkMethodTests
    {
        private StructureParameters strParms;
        private TmdParameters tmdParms;
        private TimeParameters timeParms;
        private ExcitationForces.ExcitationFunction exFunc = new ExcitationForces.ExcitationFunction(ExcitationForces.ExcitationFunctions.Sin);
        private double force = 30000000;

        public NewmarkMethodTests()
        {
            //structure parameteres
            this.strParms = new StructureParameters(6000, 10, 0.01, false);
             
            //tmd parameters
            TmdParametersCalculations tmdCalc = new TmdParametersCalculations();
            this.tmdParms = tmdCalc.CalculateAllParameters(this.strParms, 0.01);
            this.timeParms = new TimeParameters(0, 0.01);

        }


        [TestMethod()]
        public void RunNewmarkMethodTestStrDampingNotIgnored()
        {
            List<double> u1_actual = new List<double> { 0, 0.007 , 0.06, 0.246, 0.671, 1.354 };
            List<double> u2_actual = new List<double> { 0, 0.061 , 0.322, 0.842, 1.467, 1.88 };
            List<double> v1_actual = new List<double> { 0, 1.434, 9.105, 28.198, 56.631, 80.165 };
            List<double> v2_actual = new List<double> { 0, 12.11, 40.171, 63.78, 61.211, 21.391 };
            List<double> a1_actual = new List<double> { 0, 286.73, 1247.506, 2571.08, 3115.529, 1591.303 };
            List<double> a2_actual = new List<double> { 0, 2422.006, 3190.155, 1531.772, -2045.682, -5918.371 };

            double exFrequency = 9;
            NewmarkMethod nm = new NewmarkMethod(this.strParms, this.tmdParms, this.force, this.timeParms,10);
            nm.Calculate(exFrequency);
            
            List<Vector> u = nm.U;
            List<Vector> v = nm.V;
            List<Vector> a = nm.A;

            List<double> u1_expected = new List<double>();
            List<double> u2_expected = new List<double>();
            List<double> v1_expected = new List<double>();
            List<double> v2_expected = new List<double>();
            List<double> a1_expected = new List<double>();
            List<double> a2_expected = new List<double>();

            for(int i = 0;i<=5;i++)
            {
                
                u1_expected.Add(Math.Round(u[i].A1,3));
                u2_expected.Add(Math.Round(u[i].A2, 3));
                v1_expected.Add(Math.Round(v[i].A1, 3));
                v2_expected.Add(Math.Round(v[i].A2, 3));
                a1_expected.Add(Math.Round(a[i].A1, 3));
                a2_expected.Add(Math.Round(a[i].A2, 3));

            }
            CollectionAssert.AreEqual(u1_expected, u1_actual);
            CollectionAssert.AreEqual(u2_expected, u2_actual);
            CollectionAssert.AreEqual(v1_expected, v1_actual);
            CollectionAssert.AreEqual(v2_expected, v2_actual);
            CollectionAssert.AreEqual(a1_expected, a1_actual);
            CollectionAssert.AreEqual(a2_expected, a2_actual);

        }
    }

    [TestClass()]
    public class NewmarkMethodTests2
    {
        private StructureParameters strParms;
        private TmdParameters tmdParms;
        private TimeParameters timeParms;
        private ExcitationForces.ExcitationFunction exFunc = new ExcitationForces.ExcitationFunction(ExcitationForces.ExcitationFunctions.Sin);
        private double force = 30000000;

        public NewmarkMethodTests2()
        {
            //structure parameteres
            this.strParms = new StructureParameters(6000, 10, 0.1, false);

            //tmd parameters
            TmdParametersCalculations tmdCalc = new TmdParametersCalculations();
            this.tmdParms = tmdCalc.CalculateAllParameters(this.strParms, 0.01);
            this.timeParms = new TimeParameters(0, 0.00001);

        }

        [TestMethod()]
        public void RunNewmarkMethodTestStrDampingNotIgnored2()
        {

            double exFrequency = 3;
            NewmarkMethod nm = new NewmarkMethod(this.strParms, this.tmdParms, this.force, this.timeParms,0.001);
            ResultsTMD nr = nm.Calculate(exFrequency);


            double tmdA_actual = 544;
            double tmdU_actual = 1531;
            double strA_actual = 494;
            double strU_actual = 1390;

            int precision = 0;
            double tmdA_expected = Math.Round(nr.TmdA, precision);
            double tmdU_expected = Math.Round(nr.TmdU*1000, precision);
            double strA_expected = Math.Round(nr.StructureA, precision);
            double strU_expected = Math.Round(nr.StructureU*1000, precision);

            Assert.AreEqual(tmdA_expected, tmdA_actual);
            Assert.AreEqual(tmdU_expected, tmdU_actual);
            Assert.AreEqual(strA_expected, strA_actual);
            Assert.AreEqual(strU_expected, strU_actual);
        }
    }

    
    public class WriteNewmarkResultsToFile
    {
        private StructureParameters strParms;
        private TmdParameters tmdParms;
        private TimeParameters timeParms;
        private ExcitationForces.ExcitationFunction exFunc = new ExcitationForces.ExcitationFunction(ExcitationForces.ExcitationFunctions.Sin);
        private double force = 1000000;

        public WriteNewmarkResultsToFile()
        {
            //structure parameteres
            this.strParms = new StructureParameters(6000, 20, 0.02, false);

            //tmd parameters
            TmdParametersCalculations tmdCalc = new TmdParametersCalculations();
            this.tmdParms = tmdCalc.CalculateAllParameters(this.strParms, 0.05);
            this.timeParms = new TimeParameters(0, 0.001);

        }

        
        public void WriteResultsToFile()
        {

            double exFrequency = 5;
            NewmarkMethod nm = new NewmarkMethod(this.strParms, this.tmdParms, this.force, this.timeParms,0.01);
            ResultsTMD nr = nm.Calculate(exFrequency);

            //write results to file
            using (StreamWriter sw = new StreamWriter(@"G:\newmarkTestDisplacement.txt"))
            {
                List<double> time = nm.Time;
                List<double> strDispl = nm.U.ConvertAll(e => e.A2);

                //writing file
                for (int i =0;i<time.Count;i++)
                {
                    sw.WriteLine(string.Format("{0},{1}", time[i], strDispl[i]));
                }
            }
            
        }
    }

    [TestClass()]
    public class CalculationError_Newmart
    {
        private StructureParameters strParam;
        private TmdParameters tmdParam;
        private TimeParameters timeParms;
        private ExcitationForces.ExcitationFunction exFunc = new ExcitationForces.ExcitationFunction(ExcitationForces.ExcitationFunctions.Sin);
        private double force = 1000000;

        public CalculationError_Newmart()
        {
            //structure parameteres
            this.strParam = new StructureParameters(6000, 2, 0.02, false);

            //tmd parameters
            TmdParametersCalculations tmdCalc = new TmdParametersCalculations();
            this.tmdParam = tmdCalc.CalculateAllParameters(this.strParam, 0.05);
            this.timeParms = new TimeParameters(0, 0.001);

        }

        [TestMethod()]
        public void CheckCalculationError()
        {

            double exFrequency = 10;
            NewmarkMethod nm = new NewmarkMethod(this.strParam, this.tmdParam, this.force, this.timeParms, 0.01);
            ResultsTMD nr = nm.Calculate(exFrequency);

            List<Vector> error = new List<Vector>();
            Matrix2x2 m = EquationOfMotionParameters.MassMatrix(this.strParam.M,this.tmdParam.M);
            Matrix2x2 k = EquationOfMotionParameters.StiffnessMatrix(this.strParam.K,this.tmdParam.K);
            Matrix2x2 c = EquationOfMotionParameters.DampingMatrix(this.strParam.C, this.tmdParam.C);

            for (int i = 0; i <= nm.Time.Count - 1;i++ )
            {
                double time = nm.Time[i];
                //Vector p = this.loadVector(exFrequency, time);
                Vector p = nm.P[i]; //laod
                Vector a = nm.A[i];//acceleration
                Vector v = nm.V[i];//velocity
                Vector u = nm.U[i];//displacement

                Vector e = m * a + c * v + k * u - p;

                Vector e2 = new Vector(Math.Abs(e.A1), Math.Abs(e.A2));
                error.Add(e2);
            }

            double max1 = Math.Round((error.ConvertAll<double>(x => x.A1)).Max(),3);
            double max2 = Math.Round((error.ConvertAll<double>(x => x.A2)).Max(),3);

            Assert.AreEqual(0.0000d, max1);
            Assert.AreEqual(0.0000d, max2);

                

        }
        
    }

}

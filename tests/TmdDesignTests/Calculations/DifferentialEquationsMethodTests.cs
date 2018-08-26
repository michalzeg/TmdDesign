using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdDesign.Calculations;
using TmdDesign.Calculations;
using TmdDesign.SimpleClasses;
using TmdDesign.Matrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace TmdDesign.Calculations.Tests
{
    [TestClass()]
    public class DifferentialEquationsMethodTests
    {
        private StructureParameters strParam;
        private TmdParameters tmdParam;
        private TimeParameters timeParms;
        private ExcitationForces.ExcitationFunction exFunc = new ExcitationForces.ExcitationFunction(ExcitationForces.ExcitationFunctions.Sin);
        private double force = 1000000;

        public DifferentialEquationsMethodTests()
        {
            //structure parameteres
            this.strParam = new StructureParameters(6000, 2, 0.02, false);

            //tmd parameters
            var tmdCalc = new TmdParametersCalculations();
            this.tmdParam = tmdCalc.CalculateAllParameters(this.strParam, 0.05);
            this.timeParms = new TimeParameters(0, 0.0001);

        }

        [TestMethod()]
        public void CheckCalculationError()
        {

            double exFrequency = 10;
            var nm = new FiniteDifferenceMethod(this.strParam, this.tmdParam, this.force, this.timeParms, 0.01);
            var nr = nm.Calculate(exFrequency);

            var error = new List<Vector>();
            var m = EquationOfMotionParameters.MassMatrix(this.strParam.M,this.tmdParam.M);
            var k = EquationOfMotionParameters.StiffnessMatrix(this.strParam.K,this.tmdParam.K);
            var c = EquationOfMotionParameters.DampingMatrix(this.strParam.C, this.tmdParam.C);

            for (int i = 0; i <= nm.Time.Count - 1;i++ )
            {
                double time = nm.Time[i];
                //Vector p = this.loadVector(exFrequency, time);
                var p = nm.P[i]; //laod
                var a = nm.A[i];//acceleration
                var v = nm.V[i];//velocity
                var u = nm.U[i];//displacement

                var e = m * a + c * v + k * u - p;

                var e2 = new Vector(Math.Abs(e.A1), Math.Abs(e.A2));
                error.Add(e2);
            }

            var max1 = Math.Round((error.ConvertAll<double>(x => x.A1)).Max(),3);
            var max2 = Math.Round((error.ConvertAll<double>(x => x.A2)).Max(),3);

            Assert.AreEqual(0.0000d, max1);
            Assert.AreEqual(0.0000d, max2);

                

        }
    }
}

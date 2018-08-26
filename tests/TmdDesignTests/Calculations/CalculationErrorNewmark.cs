using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;
using TmdDesign.Calculations.Calculations;
using TmdDesign.Calculations.Matrix;
using TmdDesign.Calculations.Parameters;
using TmdDesign.Calculations.Results;
using TmdDesign.Calculations.Solvers;

namespace TmdDesignTests.Calculations
{
    [TestFixture]
    public class CalculationErrorNewmark
    {
        private StructureParameters strParam;
        private TmdParameters tmdParam;
        private TimeParameters timeParms;
        private ExcitationFunction exFunc = new ExcitationFunction(ExcitationFunctions.Sin);
        private double force = 1000000;

        [OneTimeSetUp]
        public void Setup()
        {
            //structure parameteres
            this.strParam = new StructureParameters(6000, 2, 0.02, false);

            //tmd parameters
            TmdParametersCalculations tmdCalc = new TmdParametersCalculations();
            this.tmdParam = tmdCalc.CalculateAllParameters(this.strParam, 0.05);
            this.timeParms = new TimeParameters(0, 0.001);
        }

        [Test]
        public void CheckCalculationError()
        {
            double exFrequency = 10;
            NewmarkMethod newmarkMethod = new NewmarkMethod(this.strParam, this.tmdParam, this.force, this.timeParms, 0.01);
            ResultsTMD results = newmarkMethod.Calculate(exFrequency);

            List<Vector> error = new List<Vector>();
            Matrix2x2 m = EquationOfMotionParameters.MassMatrix(this.strParam.Mass, this.tmdParam.Mass);
            Matrix2x2 k = EquationOfMotionParameters.StiffnessMatrix(this.strParam.Stiffness, this.tmdParam.Stiffness);
            Matrix2x2 c = EquationOfMotionParameters.DampingMatrix(this.strParam.Damping, this.tmdParam.Damping);

            for (int i = 0; i <= newmarkMethod.Time.Count() - 1; i++)
            {
                double time = newmarkMethod.Time.ElementAt(i);

                Vector p = newmarkMethod.Force.ElementAt(i);
                Vector a = newmarkMethod.Acceleration.ElementAt(i);
                Vector v = newmarkMethod.Velocity.ElementAt(i);
                Vector u = newmarkMethod.Displacement.ElementAt(i);

                Vector e = m * a + c * v + k * u - p;

                Vector e2 = new Vector(Math.Abs(e.A1), Math.Abs(e.A2));
                error.Add(e2);
            }

            double max1 = Math.Round((error.ConvertAll<double>(x => x.A1)).Max(), 3);
            double max2 = Math.Round((error.ConvertAll<double>(x => x.A2)).Max(), 3);

            Assert.AreEqual(0.0000d, max1);
            Assert.AreEqual(0.0000d, max2);
        }
    }
}
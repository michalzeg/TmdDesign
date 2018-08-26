using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdDesign.Calculations;
using TmdDesign.SimpleClasses;
using TmdDesign.Matrix;
using TmdDesign.ExcitationForces;
using NUnit.Framework;

namespace TmdDesign.Calculations.Tests
{
    [TestFixture]
    public class DifferentialEquationsMethodTests
    {
        private StructureParameters strParam;
        private TmdParameters tmdParam;
        private TimeParameters timeParms;
        private ExcitationFunction exFunc = new ExcitationForces.ExcitationFunction(ExcitationForces.ExcitationFunctions.Sin);
        private double force = 1000000;

        [OneTimeSetUp]
        public void SetUp()
        {
            this.strParam = new StructureParameters(6000, 2, 0.02, false);

            var tmdCalc = new TmdParametersCalculations();
            this.tmdParam = tmdCalc.CalculateAllParameters(this.strParam, 0.05);
            this.timeParms = new TimeParameters(0, 0.0001);
        }

        [Test]
        public void CheckCalculationError_ReturnsProperError()
        {
            double excitationFrequency = 10;
            var factorNM = new FiniteDifferenceMethod(this.strParam, this.tmdParam, this.force, this.timeParms, 0.01);
            var factorNR = factorNM.Calculate(excitationFrequency);

            var error = new List<Vector>();
            var mass = EquationOfMotionParameters.MassMatrix(this.strParam.Mass, this.tmdParam.Mass);
            var stiffness = EquationOfMotionParameters.StiffnessMatrix(this.strParam.Stiffness, this.tmdParam.Stiffness);
            var damping = EquationOfMotionParameters.DampingMatrix(this.strParam.Damping, this.tmdParam.Damping);

            for (int i = 0; i <= factorNM.Time.Count - 1; i++)
            {
                double time = factorNM.Time[i];

                var force = factorNM.Force[i];
                var acceleration = factorNM.Acceleration[i];
                var velocity = factorNM.Velocity[i];
                var displacement = factorNM.Displacement[i];

                var result = mass * acceleration + damping * velocity + stiffness * displacement - force;

                var e2 = new Vector(Math.Abs(result.A1), Math.Abs(result.A2));
                error.Add(e2);
            }

            var max1 = Math.Round((error.ConvertAll<double>(x => x.A1)).Max(), 3);
            var max2 = Math.Round((error.ConvertAll<double>(x => x.A2)).Max(), 3);

            Assert.AreEqual(0.0000, max1);
            Assert.AreEqual(0.0000, max2);
        }
    }
}
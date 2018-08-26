using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdDesign.Calculations.Solvers;

namespace TmdDesignTests.Calculations
{
    [TestFixture]
    public class FindMaxAccelerationTests
    {
        private double testFunction(double x)
        {
            var y = Math.Sin(x * 10) * (1 + Math.Exp(-x)) * 5;
            return y;
        }

        [Test]
        public void FindMaxAcceleration_ReturnsProperValue()
        {
            var maxValue = new MaxValueFinder(10, 0.00001);

            var stopLoop = false;
            var xi = 0d;
            var increment = 0.00001;
            var xi1 = xi + increment;

            while (!stopLoop)
            {
                var yi = this.testFunction(xi);
                var yi1 = this.testFunction(xi1);
                stopLoop = maxValue.FindMaxAcceleration(yi, yi1);
                xi = xi1;
                xi1 = xi + increment;
            }

            var expected = 5.000;
            var actual = maxValue.SteadyStateValue;

            Assert.AreEqual(expected, actual, 0.1);
        }
    }
}
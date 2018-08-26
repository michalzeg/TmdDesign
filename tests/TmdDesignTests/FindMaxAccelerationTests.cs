using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdDesign.FindMax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace TmdDesign.FindMax.Tests
{
    [TestClass()]
    public class FindMaxAccelerationTests
    {
        private double f(double x)
        {
            //f(x)
            double y;
            y = Math.Sin(x * 10) * (1 + Math.Exp(-x)) * 5;
            return y;
        }

        [TestMethod()]
        public void FindMaxAccelerationTest()
        {
            var maxValue = new MaxValue(10, 0.00001);

            var stopLoop = false;
            var xi = 0d;
            var inc = 0.00001;
            var xi1 = xi + inc;
            double yi;
            double yi1;
            while (!stopLoop)
            {
                
                yi = this.f(xi);
                yi1 = this.f(xi1);
                stopLoop = maxValue.FindMaxAcceleration(yi, yi1);
                xi = xi1;
                xi1 = xi + inc;
            }

            var actual = 5.000;
            var expected = Math.Round(maxValue.SteadyStateValue, 3);

            Assert.AreEqual(expected, actual);

        }

        
    }
}

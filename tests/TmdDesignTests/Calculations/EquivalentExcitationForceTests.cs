using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdDesign.ExcitationForces;
using TmdDesign.SimpleClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace TmdDesign.ExcitationForces.Tests
{
    [TestClass()]
    public class EquivalentExcitationForceTests
    {
       

        [TestMethod()]
        public void CalculateEquivalenDynamicForceTest()
        {
            double actualEquivalentDynamicForce = 41052.677;

            //inputData
            ExcitationForceInputData inputData = new ExcitationForceInputData();
            inputData.ModalMass = 211299.94;
            inputData.NaturalFrequency = 1.3685721;
            inputData.ExcitationFrequency = 1.3685721;
            inputData.DampingRatio = 0.002;
            inputData.DynamicDisplacement = 0.041544832;


            EquivalentExcitationForce eqForceCalcs = new EquivalentExcitationForce();
            double expectedEquivalentDynamicForce = Math.Round(eqForceCalcs.CalculateEquivalenDynamicForce(inputData),3);

            Assert.AreEqual(expectedEquivalentDynamicForce, actualEquivalentDynamicForce);
        }
    }
}

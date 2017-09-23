using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdDesign.SimpleClasses;

namespace TmdDesign.ExcitationForces
{
    /// <summary>
    /// Class provides definitions of functions which may act on TunedMassDamper
    /// The function is defined as the Delegate ExcitationForce
    /// </summary>
    public delegate double ExcitationFunction(double x);
    public static class ExcitationFunctions
    {

        /// <summary>
        /// Function sinus.
        /// </summary>
        /// <param name="x"> x = parametr of function in radians</param>
        /// <returns></returns>
        public static double Sin(double x)
        {
            return Math.Sin(x);
        }


    }
    /// <summary>
    /// class consists methods for calculations of equivalent dynamic force having given displacement
    /// </summary>
    public class EquivalentExcitationForce
    {
        public double DynamicStiffness { get; private set; }
        public double CalculateEquivalenDynamicForce(ExcitationForceInputData inputData)
        {

            this.DynamicStiffness = BasicDynamicCalculations.DynamicStiffness(inputData.ModalMass, inputData.NaturalFrequency);
            double dynamicFactor = BasicDynamicCalculations.DynamicFactor(inputData.ExcitationFrequency,inputData.NaturalFrequency,inputData.DampingRatio);
            double equivalenDynamicForce = BasicDynamicCalculations.EquivalentDynamicForce(inputData.DynamicDisplacement, this.DynamicStiffness, dynamicFactor);
            return equivalenDynamicForce;
        }
    }
}

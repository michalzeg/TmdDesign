using TmdDesign.SimpleClasses;

namespace TmdDesign.ExcitationForces
{
    public class EquivalentExcitationForce
    {
        public double DynamicStiffness { get; private set; }

        public double CalculateEquivalenDynamicForce(ExcitationForceInputData inputData)
        {
            this.DynamicStiffness = BasicDynamicCalculations.DynamicStiffness(inputData.ModalMass, inputData.NaturalFrequency);
            double dynamicFactor = BasicDynamicCalculations.DynamicFactor(inputData.ExcitationFrequency, inputData.NaturalFrequency, inputData.DampingRatio);
            double equivalenDynamicForce = BasicDynamicCalculations.EquivalentDynamicForce(inputData.DynamicDisplacement, this.DynamicStiffness, dynamicFactor);
            return equivalenDynamicForce;
        }
    }
}
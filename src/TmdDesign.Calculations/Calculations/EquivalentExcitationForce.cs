using TmdDesign.Calculations.Solvers;

namespace TmdDesign.Calculations.Calculations
{
    public class EquivalentExcitationForce
    {
        public double DynamicStiffness { get; private set; }
        public double DynamicForce { get; private set; }

        public void CalculateEquivalenDynamicForce(ExcitationForceInputData inputData)
        {
            this.DynamicStiffness = BasicDynamicCalculations.DynamicStiffness(inputData.ModalMass, inputData.NaturalFrequency);
            double dynamicFactor = BasicDynamicCalculations.DynamicFactor(inputData.ExcitationFrequency, inputData.NaturalFrequency, inputData.DampingRatio);
            this.DynamicForce = BasicDynamicCalculations.EquivalentDynamicForce(inputData.DynamicDisplacement, this.DynamicStiffness, dynamicFactor);
        }
    }
}
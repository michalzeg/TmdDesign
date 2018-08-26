using TmdDesign.Calculations.Calculations;

namespace TmdDesign
{
    public interface IExcitationForceView
    {
        ExcitationForceInputData InputData { get; }

        double DynamicStiffness { set; }
        double EquivalentExcitationForce { set; }
    }
}
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmdDesign.SimpleClasses
{
    public class ExcitationForceInputData
    {
        public double DynamicDisplacement { get; set; }
        public double ExcitationFrequency { get; set; }
        public double NaturalFrequency { get; set; }
        public double DampingRatio { get; set; }
        public double ModalMass { get; set; }
    }
}
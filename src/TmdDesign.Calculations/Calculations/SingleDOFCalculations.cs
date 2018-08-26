using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using TmdDesign.Calculations.Parameters;
using TmdDesign.Calculations.Results;
using TmdDesign.Calculations.Solvers;

namespace TmdDesign.Calculations.Calculations
{
    public class SingleDOFCalculations
    {
        private StructureParameters structParms;
        private ForceParameters forceParms;

        public SingleDOFCalculations(StructureParameters structParms, ForceParameters forceParms)
        {
            this.structParms = structParms;
            this.forceParms = forceParms;
        }

        public ResultsSingleDOF Calculate(double excitationFrequency)
        {
            double staticDisplacement = BasicDynamicCalculations.StaticDisplacement(this.forceParms.ForceValue, this.structParms.Stiffness);//displacement due to static loads

            double dynamicFactor = BasicDynamicCalculations.DynamicFactor(excitationFrequency, this.structParms.NaturalFrequency, this.structParms.Ksi);

            ResultsSingleDOF result = new ResultsSingleDOF();
            result.Omega = excitationFrequency;
            result.StructureDisplacement = BasicDynamicCalculations.DynamicDisplacement(staticDisplacement, dynamicFactor);//dynamic displacement
            result.StructureAcceleration = BasicDynamicCalculations.Acceleration(excitationFrequency, staticDisplacement, dynamicFactor);//acceleration

            return result;
        }
    }
}
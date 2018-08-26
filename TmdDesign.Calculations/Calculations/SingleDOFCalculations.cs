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
            double u0 = BasicDynamicCalculations.StaticDisplacement(this.forceParms.ForceValue, this.structParms.Stiffness);//displacement due to static loads

            double rd = BasicDynamicCalculations.DynamicFactor(excitationFrequency, this.structParms.NaturalFrequency, this.structParms.Ksi);

            ResultsSingleDOF res = new ResultsSingleDOF();
            res.Omega = excitationFrequency;
            res.StructureDisplacement = BasicDynamicCalculations.DynamicDisplacement(u0, rd);//dynamic displacement
            res.StructureAcceleration = BasicDynamicCalculations.Acceleration(excitationFrequency, u0, rd);//acceleration

            return res;
        }
    }
}
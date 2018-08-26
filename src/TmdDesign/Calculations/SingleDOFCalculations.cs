using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TmdDesign.SimpleClasses;
using TmdDesign.ExcitationForces;
using System.Threading.Tasks;

namespace TmdDesign.Calculations
{
    /// <summary>
    /// class calculates the accelerations and displacements without TMD
    /// </summary>
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

                double u0 = BasicDynamicCalculations.StaticDisplacement(this.forceParms.ForceValue,this.structParms.K);//displacement due to static loads

                //dynamic facor
                double rd = BasicDynamicCalculations.DynamicFactor(excitationFrequency, this.structParms.NaturalFrequency, this.structParms.Ksi);

                ResultsSingleDOF res = new ResultsSingleDOF();
                res.Omega = excitationFrequency;
                res.StructureU = BasicDynamicCalculations.DynamicDisplacement(u0, rd);//dynamic displacement
                res.StructureA = BasicDynamicCalculations.Acceleration(excitationFrequency, u0, rd);//acceleration

                return res;
            }

            

            
        
    }
}

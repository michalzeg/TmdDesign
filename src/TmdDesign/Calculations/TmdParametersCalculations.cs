using System;

namespace TmdDesign.SimpleClasses
{
    /// <summary>
    /// Class calcualtes parameters of TMD
    /// </summary>
    public class TmdParametersCalculations
    {
        /// <summary>
        /// calcualtes properties of the TMD
        /// </summary>
        /// <param name="strucParameters">class with parameters of structure </param>
        /// <param name="mi">assumed ratio of tMD to modal mass</param>
        /// <returns>class with TMD properties</returns>
        public TmdParameters CalculateAllParameters(StructureParameters strucParameters, double mi)
        {
            //parameters of TMD
            double m = this.calculateTmdMass(strucParameters.M, mi);
            double deltaOpt = this.calculateOptimumTmdFrequency(mi);
            double omegaD = this.calculateTmdFrequency(strucParameters.NaturalFrequency, deltaOpt);
            double ksiOpt = this.calculateOptimumDampingRatio(mi);

            var tmdParam = new TmdParameters(m, mi, omegaD, deltaOpt, ksiOpt);
            return tmdParam;
        }

        /// <summary>
        /// calculates the mass of the TMd
        /// </summary>
        /// <param name="m">modal mass of structure</param>
        /// <param name="mi">assumed ration of TMD to modal mass</param>
        private double calculateTmdMass(double m, double mi)
        {
            double mTmd;
            mTmd = mi * m;
            return mTmd;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mi">ratio of TMD mass to modal mass</param>
        /// <returns> optimal TMD frequency</returns>
        private double calculateOptimumTmdFrequency(double mi)
        {
            double deltaOpt;
            deltaOpt = 1 / (1 + mi);
            return deltaOpt;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="omega">natural frequency of the structure</param>
        /// <param name="deltaOpt">optimum frewuency ratio</param>
        /// <returns>TMD frewuency</returns>
        private double calculateTmdFrequency(double omega, double deltaOpt)
        {
            double omegaTmd;
            omegaTmd = deltaOpt * omega;
            return omegaTmd;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mi">ratio of TMD mass to modal mass</param>
        /// <returns> optimum damping ratio of TMD</returns>
        private double calculateOptimumDampingRatio(double mi)
        {
            double ksiOpt;
            ksiOpt = Math.Sqrt(3 * mi / (8 * Math.Pow(1 + mi, 3)));
            return ksiOpt;
        }
    }
}
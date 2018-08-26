using System;

namespace TmdDesign.Calculations.Parameters
{
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
            double mass = this.CalculateTmdMass(strucParameters.Mass, mi);
            double deltaOptimum = this.CalculateOptimumTmdFrequency(mi);
            double omegaD = this.CalculateTmdFrequency(strucParameters.NaturalFrequency, deltaOptimum);
            double ksiOpt = this.CalculateOptimumDampingRatio(mi);

            var tmdParam = new TmdParameters(mass, mi, omegaD, deltaOptimum, ksiOpt);
            return tmdParam;
        }

        private double CalculateTmdMass(double m, double mi)
        {
            var massTmd = mi * m;
            return massTmd;
        }

        private double CalculateOptimumTmdFrequency(double mi)
        {
            var deltaOpt = 1 / (1 + mi);
            return deltaOpt;
        }

        private double CalculateTmdFrequency(double omega, double deltaOpt)
        {
            var omegaTmd = deltaOpt * omega;
            return omegaTmd;
        }

        private double CalculateOptimumDampingRatio(double mi)
        {
            var ksiOpt = Math.Sqrt(3 * mi / (8 * Math.Pow(1 + mi, 3)));
            return ksiOpt;
        }
    }
}
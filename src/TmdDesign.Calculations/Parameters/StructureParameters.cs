using System;

namespace TmdDesign.Calculations.Parameters
{
    public class StructureParameters
    {
        public double Mass { get; private set; }
        public double Stiffness { get; private set; }
        public double Damping { get; private set; }
        public double NaturalFrequency { get; private set; }
        public double Ksi { get; private set; }
        public bool IgnoreStructuralDamping { get; private set; }

        /// <summary>
        /// Parameters of the structure
        /// </summary>
        /// <param name="m">modal mass of a structure</param>
        /// <param name="k">modal stiffness of a structure</param>
        /// <param name="c">modal damping of a structure</param>
        /// <param name="omega">natural frequency of a structure</param>
        /// <param name="ksi">damping ration of a structure</param>
        /// <param name="ignoreStructuralDamping">determines if damping of a structure is ignored or not</param>
        public StructureParameters(double m, double omega, double ksi, bool ignoreStructuralDamping)
        {
            this.Mass = m;
            this.NaturalFrequency = omega;
            this.Ksi = ksi;
            this.IgnoreStructuralDamping = ignoreStructuralDamping;
            this.Stiffness = this.CalculateStiffness();
            this.Damping = this.CalculateDamping();
        }

        private double CalculateDamping()
        {
            double damping;
            if (this.IgnoreStructuralDamping)
                damping = 0;
            else
                damping = 2 * this.Mass * (2 * Math.PI * this.NaturalFrequency) * this.Ksi;
            return damping;
        }

        private double CalculateStiffness()
        {
            double stiffness = Math.Pow(2 * Math.PI * this.NaturalFrequency, 2) * this.Mass;
            return stiffness;
        }
    }
}
using System;

namespace TmdDesign.SimpleClasses
{
    /// <summary>
    /// class with parameteres describing vibrating structure
    /// </summary>
    public class StructureParameters
    {
        public double M { get; private set; }
        public double K { get; private set; }
        public double C { get; private set; }
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
            this.M = m;
            this.NaturalFrequency = omega;
            this.Ksi = ksi;
            this.IgnoreStructuralDamping = ignoreStructuralDamping;
            this.K = this.calculateStiffness();
            this.C = this.calculateDamping();
        }

        private double calculateDamping()
        {
            double c;
            if (this.IgnoreStructuralDamping)
                c = 0;
            else
                c = 2 * this.M * (2 * Math.PI * this.NaturalFrequency) * this.Ksi;
            return c;
        }

        private double calculateStiffness()
        {
            double k = Math.Pow(2 * Math.PI * this.NaturalFrequency, 2) * this.M;
            return k;
        }
    }
}
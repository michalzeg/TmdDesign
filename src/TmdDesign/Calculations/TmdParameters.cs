using System;

namespace TmdDesign.SimpleClasses
{
    public class TmdParameters
    {
        public double Mass { get; private set; }
        public double Stiffness { get; private set; }
        public double Damping { get; private set; }
        public double Mi { get; private set; }
        public double OmegaD { get; private set; }
        public double DeltaOpt { get; private set; }
        public double Ksi { get; private set; }

        /// <summary>
        /// Parameters of TMD
        /// </summary>
        /// <param name="m">mass of TMD</param>
        /// <param name="k">spring stiffness of TMD</param>
        /// <param name="c">damping of TMD</param>
        /// <param name="mi">assumed ratio of TMD to modal mass</param>
        /// <param name="omegaD">TMD frequency</param>
        /// <param name="delataOpt">TMD frequency ratio</param>
        /// /// <param name="ksi">TMD damping ratio</param>
        public TmdParameters(double m, double k, double c, double mi, double omegaD, double delataOpt, double ksi)
        {
            this.Mass = m;
            this.Stiffness = k;
            this.Damping = c;
            this.Mi = mi;
            this.OmegaD = omegaD;
            this.DeltaOpt = delataOpt;
            this.Ksi = ksi;
        }

        public TmdParameters(double m, double mi, double omegaD, double delataOpt, double ksi)
        {
            this.Mi = mi;
            this.OmegaD = omegaD;
            this.DeltaOpt = delataOpt;
            this.Ksi = ksi;
            this.Mass = m;
            this.Stiffness = this.CalculateTmdSpringStiffness(m, omegaD);
            this.Damping = this.calculateTmdDamping(m, omegaD, ksi);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mTmd">mass of TMD</param>
        /// <param name="omegaTmd">natural frequency of TMD</param>
        /// <returns> spring stiffness of TMD</returns>
        private double CalculateTmdSpringStiffness(double mTmd, double omegaTmd)
        {
            var stiffness = Math.Pow(2 * Math.PI * omegaTmd, 2) * mTmd;
            return stiffness;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mTmd">madss of TMD</param>
        /// <param name="omegaTmd">natural frequency of TMD</param>
        /// <param name="ksiTmd">TMD damping ratio</param>
        /// <returns>damping</returns>
        private double calculateTmdDamping(double mTmd, double omegaTmd, double ksiTmd)
        {
            var damping = 2 * mTmd * (2 * Math.PI * omegaTmd) * ksiTmd;
            return damping;
        }
    }
}
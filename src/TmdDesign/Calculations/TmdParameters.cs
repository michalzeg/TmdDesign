using System;

namespace TmdDesign.SimpleClasses
{
    /// <summary>
    /// privides the data about TMD parameters
    /// </summary>
    public class TmdParameters
    {
        public double M { get; private set; }
        public double K { get; private set; }
        public double C { get; private set; }
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
            this.M = m;
            this.K = k;
            this.C = c;
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
            this.M = m;
            this.K = this.calculateTmdSpringStiffness(m, omegaD);
            this.C = this.calculateTmdDamping(m, omegaD, ksi);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mTmd">mass of TMD</param>
        /// <param name="omegaTmd">natural frequency of TMD</param>
        /// <returns> spring stiffness of TMD</returns>
        private double calculateTmdSpringStiffness(double mTmd, double omegaTmd)
        {
            double k;
            k = Math.Pow(2 * Math.PI * omegaTmd, 2) * mTmd;
            return k;
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
            double c;
            c = 2 * mTmd * (2 * Math.PI * omegaTmd) * ksiTmd;
            return c;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdDesign.Matrix;
using TmdDesign.ExcitationForces;

namespace TmdDesign.SimpleClasses
{
    //describes the solver (Newmark method or Differential equations method)
    public enum SolvingMethod
    {
        NewmarkMethod,
        FiniteDifferenceMethod
    };

    public interface ISolver
    {
        List<Vector> U { get; } //list of vectors with displacement
        List<Vector> V { get; } //list of vectors with velocity
        List<Vector> A { get; } //list of vectors with acceleration
        List<double> Time { get; } //list with time
        List<Vector> P { get; } //list with load

        ResultsTMD Calculate(double excitationFrequency);
    }

    /// <summary>
    /// class which wraps data on ExcitationForce
    /// </summary>
    public class ForceParameters
    {
        public double ForceValue { get; set; }
        public double ExcitationFrequencyIntervalValue { get; set; }
        public double StartFrequency { get; set; }
        public double FinalFrequency { get; set; }
    }
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

    /// <summary>
    /// class with results from Newmart analysis for current excitation (steady state conditions)
    /// </summary>
    public class ResultsTMD
    {
        public double Omega { get; set; } //excitation frequency
        public double TmdA { get; set; }//TMD acceleration
        public double TmdU { get; set; }//TMD displacement
        public double StructureA { get; set; } //structure acceleration
        public double StructureU { get; set; } //structure displacement
    }

    /// <summary>
    /// results for SDF element (without TMD)
    /// </summary>
    public class ResultsSingleDOF
    {
        public double Omega { get; set; } //excitation frequency
        public double StructureA { get; set; } //structure acceleration
        public double StructureU { get; set; } //structure displacement
    }

    public class TimeParameters
    {
        public double T0 { get; private set; }
        public double Dt { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t0">start time</param>
        /// <param name="dt">time increment</param>
        public TimeParameters(double t0, double dt)
        {
            this.T0 = t0;
            this.Dt = dt;
        }
    }

    public class Results
    {
        public List<ResultsTMD> ResultsWithTMD { get; set; }
        public List<ResultsSingleDOF> ResultsWithoutTMD { get; set; }

    }


    /// <summary>
    /// calculates the strifness, mass and damping matricies
    /// </summary>
    public static class EquationOfMotionParameters
    {
        public static Matrix2x2 MassMatrix(double strM, double tmdM)
        {
            //mass structure
            //|tmdMass | 0
            //|0       | structure mass
            Matrix2x2 m = new Matrix2x2()
            {
                A11 = tmdM,
                A12 = 0,
                A21 = 0,
                A22 = strM
            };
            return m;
        }
        public static Matrix2x2 StiffnessMatrix(double strK,double tmdK)
        {
            //mass structure
            //|tmdStiffness  | -tmdStiffness
            //|-tmdStiffness | tmdStiffnes + structureStiffness
            Matrix2x2 k = new Matrix2x2()
            {
                A11 = tmdK,
                A12 = -tmdK,
                A21 = -tmdK,
                A22 = tmdK+strK
            };
            return k;
        }
        public static Matrix2x2 DampingMatrix(double strC, double tmdC)
        {
            //mass structure
            //|tmdDamping  | -tmdDamping
            //|-tmdDamping | tmdDamping + structureDamping
            Matrix2x2 c = new Matrix2x2()
            {
                A11 = tmdC,
                A12 = -tmdC,
                A21 = -tmdC,
                A22 = strC+tmdC
            };
            return c;
        }
        public static Vector LoadVector(double omega, double t, double value, ExcitationFunction function)
        {
            //t - time
            //omega - excitation frequency
            //load vector strucutre
            //0
            //P
            Vector p = new Vector()
            {
                A1 = 0,
                A2 = value * function(2 * Math.PI * omega * t)
            };
            return p;
        }

    }


    /// <summary>
    /// provides the methods for calculations of dynamic factor, displacement
    /// </summary>
    public static class BasicDynamicCalculations
    {
        public static double DynamicFactor(double excitationFrequency, double naturalFrequency, double dampingRatio)
        {
            double rd = 1 / (Math.Sqrt(Math.Pow(1 - Math.Pow(excitationFrequency / naturalFrequency, 2), 2) + 2 * dampingRatio * Math.Pow(excitationFrequency / naturalFrequency, 2)));
            return rd;
        }

        public static double StaticDisplacement(double force, double stiffness)
        {
            double u0 = force / stiffness;
            return u0;
        }

        public static double Acceleration(double excitationFrequency, double staticDisplacement, double dynamicFactor)
        {
            return dynamicFactor * staticDisplacement * Math.Pow(Math.PI * 2 * excitationFrequency, 2);
        }

        public static double DynamicDisplacement(double staticDisplacement, double dynamicDisplacement)
        {
            return dynamicDisplacement * staticDisplacement;
        }

        public static double DynamicStiffness(double modalMass, double naturalFrequency)
        {
            double ds = Math.Pow((2 * Math.PI*naturalFrequency), 2) * modalMass;
            return ds;
        }
        public static double EquivalentDynamicForce(double displacement, double stiffness, double dynamicFactor)
        {
            double p = (displacement * stiffness) / dynamicFactor;
            return p;
        }
    }

    /// <summary>
    /// argument for backgroundworker operations
    /// </summary>
    public class BackgroundWorkerArgument
    {
        public TmdParameters TmdParms { get; set; }
        public StructureParameters StrParms { get; set; }
        public TimeParameters TimeParms { get; set; }
        public ForceParameters ForceParms { get; set; }
        public SolvingMethod SolvingMethod { get; set; }
        public bool SaveResultsToExcelFile { get; set; }
    }
    
    /// <summary>
    /// class contains input data for equivalent force calculations
    /// </summary>
    public class ExcitationForceInputData
    {
        public double DynamicDisplacement {get;set;}
        public double ExcitationFrequency { get; set; }
        public double NaturalFrequency { get; set; }
        public double DampingRatio { get; set; }
        public double ModalMass { get; set; }
    }
}


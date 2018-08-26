using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdDesign.ExcitationForces;
using TmdDesign.Matrix;
using TmdDesign.FindMax;
using TmdDesign.SimpleClasses;
//ALL UNITS in [kg], [N], and [m], [Hz]
namespace TmdDesign.Calculations
{
    
    /// <summary>
    /// DifferentialEquationsMethod class calculates the accelerations,velocities and displacements
    /// </summary>
    public class FiniteDifferenceMethod :ISolver
    {

        private int numberOfExtremes = 30;
        private double epsilon;

        private Vector u0; //starting displacement
        private Vector v0; //starting velocity

        private Matrix2x2 m; //mass matrix
        private Matrix2x2 k;//stiffness matrix
        private Matrix2x2 c;//damping matrix
  

        private StructureParameters strParam; //parameters of the structure
        private TmdParameters tmdParam; //parameters of the tmd
        private ExcitationFunction excitationFunction; //the function describing force acting on the structure
        private double excitationForceValue; //value of the force acting on structure
        private TimeParameters timeParam; //time parametres

        //properties
        public List<Vector> U { get; private set; } //list of vectors with displacement
        public List<Vector> V { get; private set; } //list of vectors with velocity
        public List<Vector> A { get; private set; } //list of vectors with acceleration
        public List<double> Time { get; private set; } //list with time
        public List<Vector> P { get; private set; } //list with load

        public FiniteDifferenceMethod(StructureParameters strParam, TmdParameters tmdParam, double excitationForceValue, TimeParameters timeParam, double epsilon)
        {
            this.u0 = new Vector(0, 0);
            this.v0 = new Vector(0, 0);

            this.strParam = strParam;
            this.tmdParam = tmdParam;
            
            this.excitationForceValue = excitationForceValue;
            this.timeParam = timeParam;
            this.epsilon = epsilon;

            this.m = EquationOfMotionParameters.MassMatrix(this.strParam.M, this.tmdParam.M);
            this.k = EquationOfMotionParameters.StiffnessMatrix(this.strParam.K, this.tmdParam.K);
            this.c = EquationOfMotionParameters.DampingMatrix(this.strParam.C, this.tmdParam.C);

            this.excitationFunction = ExcitationForces.ExcitationFunctions.Sin;
        }

        /// <summary>
        /// public method which calcualtes displacements, velocities and accelerations for given excitation force frequency
        /// </summary>
        /// <param name="excitationFrequency"> excitation force frequency</param>
        public ResultsTMD Calculate(double excitationFrequency)
        {
            // lists with results
            List<Vector> a = new List<Vector>();
            List<Vector> v = new List<Vector>();
            List<Vector> u = new List<Vector>();
            List<double> time = new List<double>();//list with time
            List<Vector> p = new List<Vector>();

            //auxiliary variables
            Vector p0 = EquationOfMotionParameters.LoadVector( excitationFrequency, this.timeParam.StartTime,this.excitationForceValue,this.excitationFunction); //force at starting time

            Vector a0 = this.m.Invert() * (p0 - this.c * v0 - this.k * u0); //starting acceleration
            Vector u_1 = u0 - this.timeParam.DeltaTime * v0 + 0.5 * this.timeParam.DeltaTime * this.timeParam.DeltaTime * a0;//displacement with -1 index
            Vector v_1 = new Vector(0, 0);
            Vector a_1 = new Vector(0, 0);
            Matrix2x2 k_ = (1 / (this.timeParam.DeltaTime * this.timeParam.DeltaTime)) * this.m + 1 / (2 * this.timeParam.DeltaTime) * this.c;
            Matrix2x2 a_ = (1 / (this.timeParam.DeltaTime * this.timeParam.DeltaTime)) * this.m - 1 / (2 * this.timeParam.DeltaTime) * this.c;
            Matrix2x2 b_ = this.k - (2 / (this.timeParam.DeltaTime * this.timeParam.DeltaTime)) * this.m;

            //u.Add(this.u0);
            //v.Add(this.v0);
            //a.Add(a0);
            //time.Add(this.timeParam.T0);
            //p.Add(p0);
            //Newmartk calculations
            int i = 0;
            
            double ti = this.timeParam.StartTime;//time i
            
            Vector ui = this.u0;
            //Vector vi = this.v0;
            //Vector ai = a0;

            MaxValueFinder tmdA = new MaxValueFinder(this.numberOfExtremes, this.epsilon);
            MaxValueFinder tmdU = new MaxValueFinder(this.numberOfExtremes, this.epsilon);
            MaxValueFinder structA = new MaxValueFinder(this.numberOfExtremes, this.epsilon);
            MaxValueFinder structU = new MaxValueFinder(this.numberOfExtremes, this.epsilon);

            bool tmdAFound = false; //determines if max acceleration and displacement of TMD has been found
            bool tmdUFound = false; 
            bool structAFound = false;
            bool structUFound = false;

            bool finishLoop = false; //determines when calculations are stopped
            while (!finishLoop)
            {
                //double ti1 = ti + this.timeParam.Dt;//time i+1
                Vector pi = EquationOfMotionParameters.LoadVector( excitationFrequency, ti,this.excitationForceValue,this.excitationFunction);
                //Vector pi1 = EquationOfMotionParameters.LoadVector(excitationFrequency, ti1, this.excitationForceValue, this.excitationFunction);

                Vector p_ = pi - a_ * u_1 - b_ * ui;
                Vector ui1 = k_.Invert() * p_;
                Vector vi = (1 / (2 * this.timeParam.DeltaTime)) * (ui1 - u_1);
                Vector ai = (1 / (this.timeParam.DeltaTime * this.timeParam.DeltaTime)) * (ui1 - 2 * ui + u_1);
                //if (i <= 5) //only for test purposes
                //{
                    u.Add(ui);
                    v.Add(vi);
                    a.Add(ai);
                    time.Add(ti);
                    p.Add(pi);
                //}
                if (!tmdAFound)
                    tmdAFound = tmdA.FindMaxAcceleration(a_1.A1, ai.A1);
                if (!tmdUFound)
                   tmdUFound = tmdU.FindMaxAcceleration(u_1.A1, ui.A1);
                if (!structAFound)
                   structAFound = structA.FindMaxAcceleration(a_1.A2, ai.A2);
                if (!structUFound)
                    structUFound = structU.FindMaxAcceleration(u_1.A2, ui.A2);

                if (tmdAFound && tmdUFound && structAFound && structUFound) //when all values are found then calculations are finished
                {
                    finishLoop = true;
                    break;
                }
                u_1 = ui; //u_i-1;
                v_1 = vi;
                a_1 = ai;
                ui = ui1;
                //ui = ui1;
                //vi = vi1;
                //ai = ai1;
                ti = ti + this.timeParam.DeltaTime;
                i++;
            }
            //******only for test purposes****///
            this.A = a;
            this.V = v;
            this.U = u;
            this.Time = time;
            this.P = p;
            //*********----******************

            ResultsTMD results = new ResultsTMD();
            results.Omega = excitationFrequency;
            results.StructureA = structA.SteadyStateValue;
            results.StructureU = structU.SteadyStateValue;
            results.TmdA = tmdA.SteadyStateValue;
            results.TmdU = tmdU.SteadyStateValue;
            return results;
            
        }
      

    }

    
}

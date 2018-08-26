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
    /// NewmarkMethod class calculates the accelerations,velocities and displacements
    /// </summary>
    public class NewmarkMethod :ISolver
    {
        private readonly double beta = 0.25; //parameters of NewmarkMethod
        private readonly double gamma = 0.5;
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

        public NewmarkMethod(StructureParameters strParam, TmdParameters tmdParam, double excitationForceValue, TimeParameters timeParam, double epsilon)
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
            Vector p0 = EquationOfMotionParameters.LoadVector( excitationFrequency, this.timeParam.T0,this.excitationForceValue,this.excitationFunction); //force at starting time
            
            Vector a0 = this.m.Invert() * (p0 - this.c * this.v0 - this.k * this.u0); //starting acceleration
            Matrix2x2 k_ = this.k + (1 / (this.beta * this.timeParam.Dt * this.timeParam.Dt)) * this.m + this.gamma / (this.beta * this.timeParam.Dt) * this.c;
            Matrix2x2 ap = 1 / (this.beta*this.timeParam.Dt) * this.m +  (this.gamma / (this.beta)) * this.c;
            Matrix2x2 bp = (1 / (2 * this.beta)) * this.m + this.timeParam.Dt * (this.gamma / (2 * this.beta) - 1) * this.c;
            u.Add(this.u0);
            v.Add(this.v0);
            a.Add(a0);
            time.Add(this.timeParam.T0);
            p.Add(p0);
            //Newmartk calculations
            int i = 0;
            
            double ti = this.timeParam.T0;//time i
            
            Vector ui = this.u0;
            Vector vi = this.v0;
            Vector ai = a0;

            MaxValue tmdA = new MaxValue(this.numberOfExtremes, this.epsilon);
            MaxValue tmdU = new MaxValue(this.numberOfExtremes, this.epsilon);
            MaxValue structA = new MaxValue(this.numberOfExtremes, this.epsilon);
            MaxValue structU = new MaxValue(this.numberOfExtremes, this.epsilon);

            bool tmdAFound = false; //determines if max acceleration and displacement of TMD has been found
            bool tmdUFound = false; 
            bool structAFound = false;
            bool structUFound = false;

            bool finishLoop = false; //determines when calculations are stopped
            while (!finishLoop)
            {
                double ti1 = ti + this.timeParam.Dt;//time i+1
                Vector pi = EquationOfMotionParameters.LoadVector( excitationFrequency, ti,this.excitationForceValue,this.excitationFunction);
                Vector pi1 = EquationOfMotionParameters.LoadVector(excitationFrequency, ti1, this.excitationForceValue, this.excitationFunction);

                Vector dpi = pi1 - pi; //increase of force
                Vector dpi_ = dpi + ap * vi + bp * ai;

                Vector dui = k_.Invert() * dpi_;
                Vector dvi = (this.gamma / (this.beta * this.timeParam.Dt)) * dui - (this.gamma / this.beta) * vi + this.timeParam.Dt * (1 - (this.gamma / (2 * this.beta))) * ai;
                Vector dai = (1 / (this.beta * this.timeParam.Dt * this.timeParam.Dt)) * dui - (1 / (this.beta * this.timeParam.Dt)) * vi - (1 / (2 * this.beta)) * ai;

                Vector ui1 = ui + dui;//i+1
                Vector vi1 = vi + dvi;//i+1
                Vector ai1 = ai + dai;//i+1
                //if (i <= 5) //only for test purposes
                //{
                    u.Add(ui1);
                    v.Add(vi1);
                    a.Add(ai1);
                    time.Add(ti1);
                    p.Add(pi1);
                //}
                if (!tmdAFound)
                    tmdAFound = tmdA.FindMaxAcceleration(ai.A1, ai1.A1);
                if (!tmdUFound)
                   tmdUFound = tmdU.FindMaxAcceleration(ui.A1, ui1.A1);
                if (!structAFound)
                   structAFound = structA.FindMaxAcceleration(ai.A2, ai1.A2);
                if (!structUFound)
                    structUFound = structU.FindMaxAcceleration(ui.A2, ui1.A2);

                if (tmdAFound && tmdUFound && structAFound && structUFound) //when all values are found then calculations are finished
                {
                    finishLoop = true;
                    break;
                }
                ui = ui1;
                vi = vi1;
                ai = ai1;
                ti = ti1;
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

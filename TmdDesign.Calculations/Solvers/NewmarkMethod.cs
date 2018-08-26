using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdDesign.Calculations.Calculations;
using TmdDesign.Calculations.Matrix;
using TmdDesign.Calculations.Parameters;
using TmdDesign.Calculations.Results;

//ALL UNITS in [kg], [N], and [m], [Hz]
namespace TmdDesign.Calculations.Solvers
{
    public class NewmarkMethod : ISolver
    {
        private readonly double beta = 0.25;
        private readonly double gamma = 0.5;
        private int numberOfExtremes = 30;
        private double epsilon;

        private Vector startDisplacement;

        private Vector startVelocity;

        private Matrix2x2 mass;
        private Matrix2x2 stifness;
        private Matrix2x2 damping;

        private StructureParameters strParam;
        private TmdParameters tmdParam;
        private ExcitationFunction excitationFunction;
        private double excitationForceValue;
        private TimeParameters timeParam;

        public IEnumerable<Vector> Displacement { get; private set; }

        public IEnumerable<Vector> Velocity { get; private set; }
        public IEnumerable<Vector> Acceleration { get; private set; }
        public IEnumerable<double> Time { get; private set; }
        public IEnumerable<Vector> Force { get; private set; }

        public NewmarkMethod(StructureParameters strParam, TmdParameters tmdParam, double excitationForceValue, TimeParameters timeParam, double epsilon)
        {
            this.startDisplacement = new Vector(0, 0);
            this.startVelocity = new Vector(0, 0);

            this.strParam = strParam;
            this.tmdParam = tmdParam;

            this.excitationForceValue = excitationForceValue;
            this.timeParam = timeParam;
            this.epsilon = epsilon;

            this.mass = EquationOfMotionParameters.MassMatrix(this.strParam.Mass, this.tmdParam.Mass);
            this.stifness = EquationOfMotionParameters.StiffnessMatrix(this.strParam.Stiffness, this.tmdParam.Stiffness);
            this.damping = EquationOfMotionParameters.DampingMatrix(this.strParam.Damping, this.tmdParam.Damping);

            this.excitationFunction = ExcitationFunctions.Sin;
        }

        public ResultsTMD Calculate(double excitationFrequency)
        {
            var acceleration = new List<Vector>();
            var velocity = new List<Vector>();
            var displacement = new List<Vector>();
            var time = new List<double>();
            var force = new List<Vector>();

            Vector p0 = EquationOfMotionParameters.LoadVector(excitationFrequency, this.timeParam.StartTime, this.excitationForceValue, this.excitationFunction); //force at starting time

            Vector a0 = this.mass.Invert() * (p0 - this.damping * this.startVelocity - this.stifness * this.startDisplacement); //starting acceleration
            Matrix2x2 k_ = this.stifness + (1 / (this.beta * this.timeParam.DeltaTime * this.timeParam.DeltaTime)) * this.mass + this.gamma / (this.beta * this.timeParam.DeltaTime) * this.damping;
            Matrix2x2 ap = 1 / (this.beta * this.timeParam.DeltaTime) * this.mass + (this.gamma / (this.beta)) * this.damping;
            Matrix2x2 bp = (1 / (2 * this.beta)) * this.mass + this.timeParam.DeltaTime * (this.gamma / (2 * this.beta) - 1) * this.damping;
            displacement.Add(this.startDisplacement);
            velocity.Add(this.startVelocity);
            acceleration.Add(a0);
            time.Add(this.timeParam.StartTime);
            force.Add(p0);
            //Newmartk calculations
            int i = 0;

            double ti = this.timeParam.StartTime;

            Vector ui = this.startDisplacement;
            Vector vi = this.startVelocity;
            Vector ai = a0;

            var tmdA = new MaxValueFinder(this.numberOfExtremes, this.epsilon);
            var tmdU = new MaxValueFinder(this.numberOfExtremes, this.epsilon);
            var structA = new MaxValueFinder(this.numberOfExtremes, this.epsilon);
            var structU = new MaxValueFinder(this.numberOfExtremes, this.epsilon);

            bool tmdAFound = false;
            bool tmdUFound = false;
            bool structAFound = false;
            bool structUFound = false;

            bool finishLoop = false;
            while (!finishLoop)
            {
                var ti1 = ti + this.timeParam.DeltaTime;
                var pi = EquationOfMotionParameters.LoadVector(excitationFrequency, ti, this.excitationForceValue, this.excitationFunction);
                var pi1 = EquationOfMotionParameters.LoadVector(excitationFrequency, ti1, this.excitationForceValue, this.excitationFunction);

                var dpi = pi1 - pi;
                var dpi_ = dpi + ap * vi + bp * ai;

                var dui = k_.Invert() * dpi_;
                var dvi = (this.gamma / (this.beta * this.timeParam.DeltaTime)) * dui - (this.gamma / this.beta) * vi + this.timeParam.DeltaTime * (1 - (this.gamma / (2 * this.beta))) * ai;
                var dai = (1 / (this.beta * this.timeParam.DeltaTime * this.timeParam.DeltaTime)) * dui - (1 / (this.beta * this.timeParam.DeltaTime)) * vi - (1 / (2 * this.beta)) * ai;

                var ui1 = ui + dui;
                var vi1 = vi + dvi;
                var ai1 = ai + dai;

                displacement.Add(ui1);
                velocity.Add(vi1);
                acceleration.Add(ai1);
                time.Add(ti1);
                force.Add(pi1);

                if (!tmdAFound)
                    tmdAFound = tmdA.FindMaxAcceleration(ai.A1, ai1.A1);
                if (!tmdUFound)
                    tmdUFound = tmdU.FindMaxAcceleration(ui.A1, ui1.A1);
                if (!structAFound)
                    structAFound = structA.FindMaxAcceleration(ai.A2, ai1.A2);
                if (!structUFound)
                    structUFound = structU.FindMaxAcceleration(ui.A2, ui1.A2);

                if (tmdAFound && tmdUFound && structAFound && structUFound)
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

            this.Acceleration = acceleration;
            this.Velocity = velocity;
            this.Displacement = displacement;
            this.Time = time;
            this.Force = force;

            ResultsTMD results = new ResultsTMD
            {
                Omega = excitationFrequency,
                StructureAcceleration = structA.SteadyStateValue,
                StructureDisplacement = structU.SteadyStateValue,
                TmdAcceleration = tmdA.SteadyStateValue,
                TmdDisplacement = tmdU.SteadyStateValue
            };
            return results;
        }
    }
}
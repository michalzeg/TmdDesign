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
    public class FiniteDifferenceMethod : ISolver
    {
        private int numberOfExtremes = 30;
        private double epsilon;

        private Vector startDisplacement;
        private Vector startVelocity;

        private Matrix2x2 mass;
        private Matrix2x2 stiffness;
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

        public FiniteDifferenceMethod(StructureParameters strParam, TmdParameters tmdParam, double excitationForceValue, TimeParameters timeParam, double epsilon)
        {
            this.startDisplacement = new Vector(0, 0);
            this.startVelocity = new Vector(0, 0);

            this.strParam = strParam;
            this.tmdParam = tmdParam;

            this.excitationForceValue = excitationForceValue;
            this.timeParam = timeParam;
            this.epsilon = epsilon;

            this.mass = EquationOfMotionParameters.MassMatrix(this.strParam.Mass, this.tmdParam.Mass);
            this.stiffness = EquationOfMotionParameters.StiffnessMatrix(this.strParam.Stiffness, this.tmdParam.Stiffness);
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

            var startForce = EquationOfMotionParameters.LoadVector(excitationFrequency, this.timeParam.StartTime, this.excitationForceValue, this.excitationFunction); //force at starting time

            var startAcceleration = this.mass.Invert() * (startForce - this.damping * startVelocity - this.stiffness * startDisplacement); //starting acceleration

            //All variable names according to Newmark algorithm
            var u_1 = startDisplacement - this.timeParam.DeltaTime * startVelocity + 0.5 * this.timeParam.DeltaTime * this.timeParam.DeltaTime * startAcceleration;//displacement with -1 index
            var v_1 = new Vector(0, 0);
            var a_1 = new Vector(0, 0);
            var k_ = (1 / (this.timeParam.DeltaTime * this.timeParam.DeltaTime)) * this.mass + 1 / (2 * this.timeParam.DeltaTime) * this.damping;
            var a_ = (1 / (this.timeParam.DeltaTime * this.timeParam.DeltaTime)) * this.mass - 1 / (2 * this.timeParam.DeltaTime) * this.damping;
            var b_ = this.stiffness - (2 / (this.timeParam.DeltaTime * this.timeParam.DeltaTime)) * this.mass;

            int i = 0;

            var ti = this.timeParam.StartTime;//time i

            var ui = this.startDisplacement;

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
                var pi = EquationOfMotionParameters.LoadVector(excitationFrequency, ti, this.excitationForceValue, this.excitationFunction);

                var p_ = pi - a_ * u_1 - b_ * ui;
                var ui1 = k_.Invert() * p_;
                var vi = (1 / (2 * this.timeParam.DeltaTime)) * (ui1 - u_1);
                var ai = (1 / (this.timeParam.DeltaTime * this.timeParam.DeltaTime)) * (ui1 - 2 * ui + u_1);

                displacement.Add(ui);
                velocity.Add(vi);
                acceleration.Add(ai);
                time.Add(ti);
                force.Add(pi);

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
                u_1 = ui;
                v_1 = vi;
                a_1 = ai;
                ui = ui1;

                ti = ti + this.timeParam.DeltaTime;
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
using System;
using TmdDesign.Matrix;
using TmdDesign.ExcitationForces;

namespace TmdDesign.SimpleClasses
{
    public static class EquationOfMotionParameters
    {
        public static Matrix2x2 MassMatrix(double structureMass, double tmdMass)
        {
            //mass structure
            //|tmdMass | 0
            //|0       | structure mass
            Matrix2x2 mass = new Matrix2x2()
            {
                A11 = tmdMass,
                A12 = 0,
                A21 = 0,
                A22 = structureMass
            };
            return mass;
        }

        public static Matrix2x2 StiffnessMatrix(double structureStiffness, double tmdStiffness)
        {
            //stiffness structure
            //|tmdStiffness  | -tmdStiffness
            //|-tmdStiffness | tmdStiffnes + structureStiffness
            Matrix2x2 stiffness = new Matrix2x2()
            {
                A11 = tmdStiffness,
                A12 = -tmdStiffness,
                A21 = -tmdStiffness,
                A22 = tmdStiffness + structureStiffness
            };
            return stiffness;
        }

        public static Matrix2x2 DampingMatrix(double structureDamping, double tmdDamping)
        {
            //damping structure
            //|tmdDamping  | -tmdDamping
            //|-tmdDamping | tmdDamping + structureDamping
            Matrix2x2 damping = new Matrix2x2()
            {
                A11 = tmdDamping,
                A12 = -tmdDamping,
                A21 = -tmdDamping,
                A22 = structureDamping + tmdDamping
            };
            return damping;
        }

        public static Vector LoadVector(double omega, double time, double forceValue, ExcitationFunction function)
        {
            Vector vector = new Vector()
            {
                A1 = 0,
                A2 = forceValue * function(2 * Math.PI * omega * time)
            };
            return vector;
        }
    }
}
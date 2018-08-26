using System;
using TmdDesign.Matrix;
using TmdDesign.ExcitationForces;

namespace TmdDesign.SimpleClasses
{
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

        public static Matrix2x2 StiffnessMatrix(double strK, double tmdK)
        {
            //mass structure
            //|tmdStiffness  | -tmdStiffness
            //|-tmdStiffness | tmdStiffnes + structureStiffness
            Matrix2x2 k = new Matrix2x2()
            {
                A11 = tmdK,
                A12 = -tmdK,
                A21 = -tmdK,
                A22 = tmdK + strK
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
                A22 = strC + tmdC
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
}
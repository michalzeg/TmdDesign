using System.Collections.Generic;

namespace TmdDesign.Matrix
{
    public class Vector
    {
        public double A1 { get; set; }
        public double A2 { get; set; }

        public Vector()
        {
        }

        public Vector(double a1, double a2)
        {
            this.A1 = a1;
            this.A2 = a2;
        }

        public List<double> ToList()
        {
            return new List<double>
            {
                this.A1,
                this.A2
            };
        }

        /// <summary>
        /// Multiplying matrix by vector
        /// </summary>
        /// <param name="m">matrix</param>
        /// <param name="w">vector</param>
        /// <returns></returns>
        public static Vector operator *(Matrix2x2 m, Vector w)
        {
            Vector tempVector = new Vector();
            tempVector.A1 = m.A11 * w.A1 + m.A12 * w.A2;
            tempVector.A2 = m.A21 * w.A1 + m.A22 * w.A2;
            return tempVector;
        }

        public static Vector operator *(Vector w, double x)
        {
            Vector tempVector = new Vector();
            tempVector.A1 = w.A1 * x;
            tempVector.A2 = w.A2 * x;
            return tempVector;
        }

        public static Vector operator *(double x, Vector w)
        {
            Vector tempVector = new Vector();
            tempVector.A1 = w.A1 * x;
            tempVector.A2 = w.A2 * x;
            return tempVector;
        }

        public static Vector operator +(Vector w1, Vector w2)
        {
            Vector tempVector = new Vector();
            tempVector.A1 = w1.A1 + w2.A1;
            tempVector.A2 = w1.A2 + w2.A2;
            return tempVector;
        }

        public static Vector operator -(Vector w1, Vector w2)
        {
            Vector tempVector = new Vector();
            tempVector.A1 = w1.A1 - w2.A1;
            tempVector.A2 = w1.A2 - w2.A2;
            return tempVector;
        }
    }
}
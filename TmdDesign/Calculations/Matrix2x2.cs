using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmdDesign.Matrix
{
    /// <summary>
    /// Matrix class
    /// </summary>
    public class Matrix2x2
    {
        //Matrix fields
        //|a11|a12
        //|a21|a22
        #region Private statements
        private double a11;
        private double a12;
        private double a21;
        private double a22;
        private double determinant;
        private double eigenvalue1;
        private double eigenvalue2;
        private double trace;
        #endregion

        #region Properties
        public double A11 { get { return this.a11; } set { this.a11 = value; } }
        public double A12 { get { return this.a12; } set { this.a12 = value; } }
        public double A21 { get { return this.a21; } set { this.a21 = value; } }
        public double A22 { get { return this.a22; } set { this.a22 = value; } }
        public double Eigenvalue1 
        { 
            get 
            {
                if (double.IsNaN(this.eigenvalue1))
                    this.calculateEigenvalues();
                return this.eigenvalue1; 
            } 
        }
        public double Eigenvalue2 
        { 
            get 
            {
                if (double.IsNaN(this.eigenvalue2))
                    this.calculateEigenvalues();
                return this.eigenvalue2; 
            } 
        }
        #endregion

        #region constructors
        public Matrix2x2() //default constructor
        {
            this.a11 = double.NaN;
            this.a12 = double.NaN;
            this.a21 = double.NaN;
            this.a22 = double.NaN;
            this.setDefaultValues();
        }
        public Matrix2x2(double a11, double a12, double a22) 
        {
            //symetrical matrix
            this.a11 = a11;
            this.a12 = a12;
            this.a21 = a12;
            this.a22 = a22;
            this.setDefaultValues();
        }   
        public Matrix2x2(double a11,double a12, double a21,double a22) 
        {
            this.a11 = a11;
            this.a12 = a12;
            this.a21 = a21;
            this.a22 = a22;
            this.setDefaultValues();
        }   
        #endregion
        #region Public methods
        /// <summary>
        /// Inverts matrix so that A = A^-1
        /// </summary>
        public Matrix2x2 Invert()
        {
            Matrix2x2 tempMatrix = new Matrix2x2();
            //check if determinant has been calculates
            if (double.IsNaN(this.determinant))
                this.calculateDeterminant();
            //check if matrix is singular (determinant is zero)
            if (this.determinant == 0)
            {
                //the inverse matrix does not exsist
                return tempMatrix;
            }
            double factor = 1 / this.determinant;
            //matrix syntax
            //a|b
            //c|d
            //inverse matrix
            double a = this.a11;
            double b = this.a12;
            double c = this.a21;
            double d = this.a22;

            tempMatrix.a11 = factor * d;
            tempMatrix.a12 = factor * -b;
            tempMatrix.a21 = factor * -c;
            tempMatrix.a22 = factor * a;
            return tempMatrix;
        }
        #endregion

        #region Private methods
        private void setDefaultValues()
        {
            this.trace = double.NaN;
            this.determinant = double.NaN;
            this.eigenvalue1 = double.NaN;
            this.eigenvalue2 = double.NaN;
        }
        private void calculateDeterminant()
        {
            this.determinant = this.a11 * this.a22 - this.a12 * this.a21;
            
        }
        private void calculateTrace()
        {
            //caluculate the sum of the values at the diagonal
            this.trace = this.a11 + this.a22;
            
        }
        private void calculateEigenvalues()
        {
            
            //http://www.math.harvard.edu/archive/21b_fall_04/exhibits/2dmatrices/index.html
            //first eigenvalue
            if (double.IsNaN(this.trace))
                this.calculateTrace();
            if (double.IsNaN(this.determinant))
                this.calculateDeterminant();
            double ev1 = this.trace / 2 + Math.Sqrt(this.trace * this.trace / 4 - this.determinant);
            double ev2 = this.trace / 2 - Math.Sqrt(this.trace * this.trace / 4 - this.determinant);
            
            //sorting eigenvalues
            if (ev1>ev2)
            {
                this.eigenvalue1 = ev2;
                this.eigenvalue2 = ev1;
            }
            else
            {
                this.eigenvalue1 = ev1;
                this.eigenvalue2 = ev2;
            }

        }
        #endregion

        #region static methods
        public static Matrix2x2 operator +(Matrix2x2 m1,Matrix2x2 m2)
        {
            Matrix2x2 tempMatrix = new Matrix2x2();
            tempMatrix.A11 = m1.A11 + m2.A11;
            tempMatrix.A12 = m1.A12 + m2.A12;
            tempMatrix.A21 = m1.A21 + m2.A21;
            tempMatrix.A22 = m1.A22 + m2.A22;
            return tempMatrix;
        }
        /// <summary>
        /// subtrackting two matrices
        /// </summary>
        /// <param name="m1">matrix 1</param>
        /// <param name="m2">matrix 2</param>
        /// <returns>matrix</returns>
        public static Matrix2x2 operator -(Matrix2x2 m1, Matrix2x2 m2)
        {
            Matrix2x2 tempMatrix = new Matrix2x2();
            tempMatrix.A11 = m1.A11 - m2.A11;
            tempMatrix.A12 = m1.A12 - m2.A12;
            tempMatrix.A21 = m1.A21 - m2.A21;
            tempMatrix.A22 = m1.A22 - m2.A22;
            return tempMatrix;
        }
        /// <summary>
        /// Multiplying matrix by number
        /// </summary>
        /// <param name="m">matrix</param>
        /// <param name="w">number</param>
        /// <returns>vector</returns>
        
        public static Matrix2x2 operator *(double x, Matrix2x2 w)
        {
            Matrix2x2  tempMatrix = new Matrix2x2();
            tempMatrix.A11 = w.A11 * x;
            tempMatrix.A12 = w.A12 * x;
            tempMatrix.A21 = w.A21 * x;
            tempMatrix.A22 = w.A22 * x;
            return tempMatrix;
        }
        public static Matrix2x2 operator *(Matrix2x2 w,double x)
        {
            Matrix2x2 tempMatrix = new Matrix2x2();
            tempMatrix.A11 = w.A11 * x;
            tempMatrix.A12 = w.A12 * x;
            tempMatrix.A21 = w.A21 * x;
            tempMatrix.A22 = w.A22 * x;
            return tempMatrix;
        }
        #endregion
    }

    /// <summary>
    /// basic class for two elements vector
    /// </summary>
    public class Vector
    {
        /// <summary>
        /// Vactor class
        /// a1
        /// --
        /// a2
        /// </summary>
        private double a1;
        private double a2;

        public double A1 { get { return this.a1; } set { this.a1 = value; } }
        public double A2 { get { return this.a2; } set { this.a2 = value; } }

        public Vector() { }
        public Vector(double a1, double a2)
        {
            this.a1 = a1;
            this.a2 = a2;
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
        public static Vector operator *(Vector w,double x)
        {
            Vector tempVector = new Vector();
            tempVector.A1 = w.A1 * x;
            tempVector.A2 = w.A2 * x;
            return tempVector;
        }
        public static Vector operator *(double x,Vector w)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmdDesign.Matrix
{
    public class Matrix2x2
    {
        #region Private statements

        private double determinant;
        private double eigenvalue1;
        private double eigenvalue2;
        private double trace;

        #endregion

        #region Properties

        public double A11 { get; set; }
        public double A12 { get; set; }
        public double A21 { get; set; }
        public double A22 { get; set; }

        public double Eigenvalue1
        {
            get
            {
                if (double.IsNaN(this.eigenvalue1))
                    this.CalculateEigenvalues();
                return this.eigenvalue1;
            }
        }

        public double Eigenvalue2
        {
            get
            {
                if (double.IsNaN(this.eigenvalue2))
                    this.CalculateEigenvalues();
                return this.eigenvalue2;
            }
        }

        #endregion

        #region constructors

        public Matrix2x2()
        {
            this.A11 = double.NaN;
            this.A12 = double.NaN;
            this.A21 = double.NaN;
            this.A22 = double.NaN;
            this.SetDefaultValues();
        }

        public Matrix2x2(double a11, double a12, double a21, double a22)
        {
            this.A11 = a11;
            this.A12 = a12;
            this.A21 = a21;
            this.A22 = a22;
            this.SetDefaultValues();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Inverts matrix so that A = A^-1
        /// </summary>
        public Matrix2x2 Invert()
        {
            Matrix2x2 tempMatrix = new Matrix2x2();

            if (double.IsNaN(this.determinant))
                this.CalculateDeterminant();

            if (this.determinant == 0)
            {
                return tempMatrix;
            }
            double factor = 1 / this.determinant;

            double a = this.A11;
            double b = this.A12;
            double c = this.A21;
            double d = this.A22;

            tempMatrix.A11 = factor * d;
            tempMatrix.A12 = factor * -b;
            tempMatrix.A21 = factor * -c;
            tempMatrix.A22 = factor * a;
            return tempMatrix;
        }

        public List<double> ToList()
        {
            return new List<double>
            {
                this.A11,
                this.A12,
                this.A21,
                this.A22
            };
        }

        #endregion

        #region Private methods

        private void SetDefaultValues()
        {
            this.trace = double.NaN;
            this.determinant = double.NaN;
            this.eigenvalue1 = double.NaN;
            this.eigenvalue2 = double.NaN;
        }

        private void CalculateDeterminant()
        {
            this.determinant = this.A11 * this.A22 - this.A12 * this.A21;
        }

        private void calculateTrace()
        {
            this.trace = this.A11 + this.A22;
        }

        /// <summary>
        /// Calculations according to
        /// http://www.math.harvard.edu/archive/21b_fall_04/exhibits/2dmatrices/index.html
        /// </summary>
        private void CalculateEigenvalues()
        {
            if (double.IsNaN(this.trace))
                this.calculateTrace();
            if (double.IsNaN(this.determinant))
                this.CalculateDeterminant();
            double ev1 = this.trace / 2 + Math.Sqrt(this.trace * this.trace / 4 - this.determinant);
            double ev2 = this.trace / 2 - Math.Sqrt(this.trace * this.trace / 4 - this.determinant);

            if (ev1 > ev2)
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

        public static Matrix2x2 operator +(Matrix2x2 m1, Matrix2x2 m2)
        {
            Matrix2x2 tempMatrix = new Matrix2x2();
            tempMatrix.A11 = m1.A11 + m2.A11;
            tempMatrix.A12 = m1.A12 + m2.A12;
            tempMatrix.A21 = m1.A21 + m2.A21;
            tempMatrix.A22 = m1.A22 + m2.A22;
            return tempMatrix;
        }

        public static Matrix2x2 operator -(Matrix2x2 m1, Matrix2x2 m2)
        {
            Matrix2x2 tempMatrix = new Matrix2x2();
            tempMatrix.A11 = m1.A11 - m2.A11;
            tempMatrix.A12 = m1.A12 - m2.A12;
            tempMatrix.A21 = m1.A21 - m2.A21;
            tempMatrix.A22 = m1.A22 - m2.A22;
            return tempMatrix;
        }

        public static Matrix2x2 operator *(double x, Matrix2x2 w)
        {
            Matrix2x2 tempMatrix = new Matrix2x2();
            tempMatrix.A11 = w.A11 * x;
            tempMatrix.A12 = w.A12 * x;
            tempMatrix.A21 = w.A21 * x;
            tempMatrix.A22 = w.A22 * x;
            return tempMatrix;
        }

        public static Matrix2x2 operator *(Matrix2x2 w, double x)
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
}
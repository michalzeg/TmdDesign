using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdDesign.Matrix;

namespace TmdDesign.Matrix.Tests
{
    [TestFixture]
    public class Matrix2x2Tests
    {
        private Matrix2x2 matrix1 = new Matrix2x2()
        {
            A11 = 1,
            A12 = -2,
            A21 = -2,
            A22 = 3
        };

        private Matrix2x2 matrix2 = new Matrix2x2()
        {
            A11 = 2,
            A12 = 2,
            A21 = 2,
            A22 = 2
        };

        private Vector vector1 = new Vector()
        {
            A1 = 2,
            A2 = 1
        };

        private Vector vector2 = new Vector()
        {
            A1 = 1,
            A2 = 2
        };

        [Test]
        public void EigenValues_Case1()
        {
            var a11 = -1;
            var a12 = 0;
            var a21 = 0;
            var a22 = -4;

            var estimatedEV1 = -4;
            var estimatedEV2 = -1;

            var matrix = new Matrix2x2(a11, a12, a21, a22);
            var resultEV1 = matrix.Eigenvalue1;
            var resultEV2 = matrix.Eigenvalue2;

            Assert.AreEqual(resultEV1, estimatedEV1, 0.1);
            Assert.AreEqual(resultEV2, estimatedEV2, 0.1);
        }

        [Test]
        public void EigenValues_Case2()
        {
            var a11 = -1;
            var a12 = 0;
            var a21 = 0;
            var a22 = -4;

            var estimatedEV1 = -4;
            var estimatedEV2 = -1;

            var matr = new Matrix2x2
            {
                A11 = a11,
                A12 = a12,
                A21 = a21,
                A22 = a22
            };

            var resultEV1 = matr.Eigenvalue1;
            var resultEV2 = matr.Eigenvalue2;

            Assert.AreEqual(resultEV1, estimatedEV1, 0.1);
            Assert.AreEqual(resultEV2, estimatedEV2, 0.1);
        }

        [Test]
        public void AddingTwoMatrices()
        {
            var actual = new List<double>() { 3.0, 0.0, 0.0, 5.0 };

            var expMatrix = matrix1 + matrix2;
            var expected = expMatrix.ToList();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void SubtracktingTwoMatrices()
        {
            List<double> actual = new List<double>() { -1, -4, -4, 1 };

            Matrix2x2 expMatrix = matrix1 - matrix2;
            List<double> expected = expMatrix.ToList();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void MuliplyingMatrixByNumber()
        {
            List<double> actual = new List<double>() { -2, 4, 4, -6 };

            Matrix2x2 expMatrix = 2.0 * matrix1 * (-1.0);
            List<double> expected = expMatrix.ToList();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void MatrixInversion()
        {
            List<double> actual = new List<double>() { -3, -2, -2, -1 };

            Matrix2x2 expMatrix = this.matrix1.Invert();
            List<double> expected = expMatrix.ToList();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void SingularMatrixInversion()
        {
            List<double> actual = new List<double>() { double.NaN, double.NaN, double.NaN, double.NaN };

            Matrix2x2 singularMatrix = new Matrix2x2(2, 2, 2, 2);
            Matrix2x2 expMatrix = singularMatrix.Invert();
            List<double> expected = expMatrix.ToList();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void AddingEmptyMatrixToExistingMatrix()
        {
            List<double> actual = new List<double>() { double.NaN, double.NaN, double.NaN, double.NaN };

            Matrix2x2 emptyMatrix = new Matrix2x2();
            Matrix2x2 expMatrix = this.matrix1 + emptyMatrix;
            List<double> expected = expMatrix.ToList();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void MuliplyingMatrixByVector()
        {
            List<double> actual = new List<double>() { 0, -1 };
            Vector expVector = matrix1 * vector1;
            List<double> expected = expVector.ToList();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void AddingTwoVectors()
        {
            List<double> actual = new List<double>() { 3, 3 };

            Vector expVector = this.vector1 + this.vector2;
            List<double> expected = expVector.ToList();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void SubtracktingTwoVectors()
        {
            List<double> actual = new List<double>() { 1, -1 };

            Vector expVector = this.vector1 - this.vector2;
            List<double> expected = expVector.ToList();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void MulitplyingVectorByNumber()
        {
            List<double> actual = new List<double>() { -16, -8 };

            Vector expVector = 4.0 * this.vector1 * (-2);
            List<double> expected = expVector.ToList();
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
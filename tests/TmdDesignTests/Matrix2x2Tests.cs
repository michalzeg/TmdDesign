using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdDesign.Matrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace TmdDesign.Matrix.Tests
{
    [TestClass()]
    public class Matrix2x2Tests
    {
        private Matrix2x2 m1 = new Matrix2x2()
        {
            A11 = 1,
            A12 = -2,
            A21 = -2,
            A22 = 3
        };
        private Matrix2x2 m2 = new Matrix2x2()
        {
            A11 = 2,
            A12 = 2,
            A21 = 2,
            A22 = 2
        };
        private Vector w1 = new Vector()
        {
            A1 = 2,
            A2 = 1
        };
        private Vector w2 = new Vector()
        {
            A1 = 1,
            A2 = 2
        };
        /// <summary>
        /// auxiliary function to convert matrix or vector properties to list
        /// </summary>
        /// <param name="m"></param>
        /// <returns>List</returns>
        private List<double> objectToList(Matrix2x2 m)
        {
            List<double> tempList = new List<double>();
            tempList.Add(m.A11);
            tempList.Add(m.A12);
            tempList.Add(m.A21);
            tempList.Add(m.A22);
            return tempList;
        }
        private List<double> objectToList(Vector w)
        {
            List<double> tempList = new List<double>();
            tempList.Add(w.A1);
            tempList.Add(w.A2);
            return tempList;
        }

        [TestMethod()]
        public void EigenValues1()
        {
            double a11 = -1;
            double a12 = 0;
            double a21 = 0;
            double a22 = -4;

            double estimatedEV1 = -4;
            double estimatedEV2 = -1;

            Matrix2x2 matr = new Matrix2x2(a11, a12, a21, a22);
            double resultEV1 = Math.Round(matr.Eigenvalue1,3);
            double resultEV2 = Math.Round(matr.Eigenvalue2,3);
            
            Assert.AreEqual(resultEV1, estimatedEV1);
            Assert.AreEqual(resultEV2, estimatedEV2);
        }
        [TestMethod()]
        public void EigenValues2()
        {
            double a11 = -1;
            double a12 = 0;
            double a21 = 0;
            double a22 = -4;

            double estimatedEV1 = -4;
            double estimatedEV2 = -1;

            Matrix2x2 matr = new Matrix2x2();
            matr.A11 = a11;
            matr.A12 = a12;
            matr.A21 = a21;
            matr.A22 = a22;

            double resultEV1 = Math.Round(matr.Eigenvalue1, 3);
            double resultEV2 = Math.Round(matr.Eigenvalue2, 3);

            Assert.AreEqual(resultEV1, estimatedEV1);
            Assert.AreEqual(resultEV2, estimatedEV2);
        }


        [TestMethod()]
        public void AddingTwoMatrices()
        {
            List<double> actual = new List<double>(){3.0, 0.0, 0.0, 5.0};

            Matrix2x2 expMatrix = m1+m2;
            List<double> expected = this.objectToList(expMatrix);
            //var exp = Like
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void SubtracktingTwoMatrices()
        {
            List<double> actual = new List<double>() { -1, -4, -4, 1 };

            Matrix2x2 expMatrix = m1 - m2;
            List<double> expected = this.objectToList(expMatrix);
            //var exp = Like
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void MuliplyingMatrixByNumber()
        {
            List<double> actual = new List<double>() { -2, 4, 4, -6 };

            Matrix2x2 expMatrix = 2.0 * m1 * (-1.0);
            List<double> expected = this.objectToList(expMatrix);

            CollectionAssert.AreEqual(expected, actual);
            
        }
        [TestMethod()]
        public void MatrixInversion()
        {
            List<double> actual = new List<double>() { -3, -2, -2, -1 };

            Matrix2x2 expMatrix = this.m1.Invert();
            List<double> expected = this.objectToList(expMatrix);

            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void SingularMatrixInversion()
        {
            List<double> actual = new List<double>() { double.NaN, double.NaN, double.NaN, double.NaN };

            Matrix2x2 singularMatrix = new Matrix2x2(2, 2, 2, 2);
            Matrix2x2 expMatrix = singularMatrix.Invert();
            List<double> expected = this.objectToList(expMatrix);

            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void AddingEmptyMatrixToExistingMatrix()
        {
            List<double> actual = new List<double>() { double.NaN, double.NaN, double.NaN, double.NaN };

            Matrix2x2 emptyMatrix = new Matrix2x2();
            Matrix2x2 expMatrix = this.m1 + emptyMatrix;
            List<double> expected = this.objectToList(expMatrix);

            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void MuliplyingMatrixByVector()
        {
            List<double> actual = new List<double>() {0,-1};
            Vector expVector = m1 * w1;
            List<double> expected = this.objectToList(expVector);
            CollectionAssert.AreEqual(expected, actual);

        }
        

        [TestMethod()]
        public void AddingTwoVectors()
        {
            List<double> actual = new List<double>() { 3, 3 };

            Vector expVector = this.w1 + this.w2;
            List<double> expected = this.objectToList(expVector);
            CollectionAssert.AreEqual(expected, actual);

            //Assert.Fail();
        }
        [TestMethod()]
        public void SubtracktingTwoVectors()
        {
            List<double> actual = new List<double>() { 1, -1 };

            Vector expVector = this.w1 - this.w2;
            List<double> expected = this.objectToList(expVector);
            CollectionAssert.AreEqual(expected, actual);

            //Assert.Fail();
        }
        [TestMethod()]
        public void MulitplyingVectorByNumber()
        {
            List<double> actual = new List<double>() { -16, -8 };

            Vector expVector = 4.0 * this.w1 * (-2);
            List<double> expected = this.objectToList(expVector);
            CollectionAssert.AreEqual(expected, actual);

            //Assert.Fail();
        }
    }
}

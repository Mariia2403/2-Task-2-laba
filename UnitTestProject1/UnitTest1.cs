using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using _2_лаба;
namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestConstructorFromStringArray_NonNumericValues()
        {
            string[] stringArrayWithText = { "1 2", "3 a" }; // Non-numeric value "a"

            try
            {
                new MyMatrix(stringArrayWithText);
                Assert.Fail("Expected a FormatException for non-numeric value in string array.");
            }
            catch (FormatException)
            {
                // Expected exception caught
            }
        }
        [TestMethod]
        public void TestCopyConstructor_NullInput()
        {
            MyMatrix myMatrix = null;

            try
            {
                new MyMatrix(myMatrix);
                Assert.Fail("Expected an ArgumentNullException when copying a null MyMatrix instance.");
            }
            catch (ArgumentNullException)
            {
                // Expected exception caught
            }
        }
        [TestMethod]

        public void TestSetElement_InvalidIndices()
        {
            double[,] matrix = { { 1, 2 }, { 3, 4 } };
            MyMatrix myMatrix = new MyMatrix(matrix);

            try
            {
                myMatrix.SetElement(3, 0, 10);
                Assert.Fail("Expected an ArgumentException when setting an element with out-of-bounds indices.");
            }
            catch (ArgumentException)
            {
                // Expected exception caught
            }
        }
        [TestMethod]
        public void TestGetElement_InvalidIndices()
        {
            double[,] matrix = { { 1, 2 }, { 3, 4 } };
            MyMatrix myMatrix = new MyMatrix(matrix);

            try
            {
                myMatrix.GetElement(2, 2);
                Assert.Fail("Expected an ArgumentException when getting an element with out-of-bounds indices.");
            }
            catch (ArgumentException)
            {
                // Expected exception caught
            }
        }
        [TestMethod]
        public void TestConstructorFromJaggedArray_EmptyJaggedArray()
        {
            double[][] emptyJaggedArray = new double[0][];

            try
            {
                new MyMatrix(emptyJaggedArray);
                Assert.Fail("Expected an ArgumentException for empty jagged array input.");
            }
            catch (ArgumentException)
            {
                // Expected exception caught
            }
        }
        [TestMethod]
        public void TestConstructorFromString_EmptyString()
        {
            string emptyString = "";

            try
            {
                new MyMatrix(emptyString);
                Assert.Fail("Expected an ArgumentException for empty string input.");
            }
            catch (ArgumentException)
            {
                // Expected exception caught
            }
        }
        [TestMethod]
        public void TestMatrixAddition_ValidMatrices()
        {
            double[,] matrix1 = { { 1, 2 }, { 3, 4 } };
            double[,] matrix2 = { { 5, 6 }, { 7, 8 } };

            MyMatrix m1 = new MyMatrix(matrix1);
            MyMatrix m2 = new MyMatrix(matrix2);

            MyMatrix result = m1 + m2;

            double[,] expected = { { 6, 8 }, { 10, 12 } };
            for (int i = 0; i < result.GetHeight(); i++)
            {
                for (int j = 0; j < result.GetWidth(); j++)
                {
                    Assert.AreEqual(expected[i, j], result[i, j]);
                }
            }
        }
        [TestMethod]

        public void TestMatrixAddition_InvalidDimensions()
        {
            double[,] matrix1 = { { 1, 2 } };
            double[,] matrix2 = { { 3, 4 }, { 5, 6 } };

            MyMatrix m1 = new MyMatrix(matrix1);
            MyMatrix m2 = new MyMatrix(matrix2);

            try
            {
                MyMatrix result = m1 + m2;
                Assert.Fail("Expected ArgumentException due to mismatched dimensions.");
            }
            catch (ArgumentException)
            {
                // Expected exception
            }
        }
        [TestMethod]
        public void TestMatrixMultiplication_ValidDimensions()
        {
            double[,] matrix1 = { { 2, 2, 2 }, { 2, 2, 2 } };
            double[,] matrix2 = { { 1, 1 }, { 1, 1 }, { 1, 1 } };

            MyMatrix m1 = new MyMatrix(matrix1);
            MyMatrix m2 = new MyMatrix(matrix2);

            MyMatrix result = m1 * m2;

            double[,] expected = { { 6, 6 }, { 6, 6 } };
            for (int i = 0; i < result.GetHeight(); i++)
            {
                for (int j = 0; j < result.GetWidth(); j++)
                {
                    Assert.AreEqual(expected[i, j], result[i, j]);
                }
            }
        }
        [TestMethod]
        public void TestMatrixMultiplication_InvalidDimensions()
        {
            double[,] matrix1 = { { 1, 2 } };
            double[,] matrix2 = { { 3, 4 }, { 5, 6 }, { 7, 8 } };

            MyMatrix m1 = new MyMatrix(matrix1);
            MyMatrix m2 = new MyMatrix(matrix2);

            try
            {
                MyMatrix result = m1 * m2;
                Assert.Fail("Expected ArgumentException due to invalid dimensions for multiplication.");
            }
            catch (ArgumentException)
            {
                // Expected exception
            }
        }
        [TestMethod]

        public void TestTransposeMethod()
        {
            double[,] matrix = { { 1, 2, 3 }, { 4, 5, 6 } };
            MyMatrix m = new MyMatrix(matrix);

            MyMatrix transposed = m.GetTransponedCopy(m);

            double[,] expected = { { 1, 4 }, { 2, 5 }, { 3, 6 } };
            for (int i = 0; i < transposed.GetHeight(); i++)
            {
                for (int j = 0; j < transposed.GetWidth(); j++)
                {
                    Assert.AreEqual(expected[i, j], transposed[i, j]);
                }
            }
        }
        [TestMethod]
        public void TestTransposeInPlace()
        {
            double[,] matrix = { { 1, 2, 3 }, { 4, 5, 6 } };
            MyMatrix m = new MyMatrix(matrix);

            m.TransponedMe();

            double[,] expected = { { 1, 4 }, { 2, 5 }, { 3, 6 } };
            for (int i = 0; i < m.GetHeight(); i++)
            {
                for (int j = 0; j < m.GetWidth(); j++)
                {
                    Assert.AreEqual(expected[i, j], m[i, j]);
                }
            }
        }
        [TestMethod]
        public void TestCalculateDeterminant_ValidSquareMatrix()
        {
            double[,] matrix = { { 5, 6 }, { 7, 8 } };
            MyMatrix m = new MyMatrix(matrix);

            double determinant = m.CalcDeterminant();
            Assert.AreEqual(-2, determinant, 1e-10);//1e-10 це рівень допустимої похибки
        }
        [TestMethod]
        public void TestCalculateDeterminant_NonSquareMatrix()
        {
            double[,] matrix = { { 1, 2, 3 }, { 4, 5, 6 } };
            MyMatrix m = new MyMatrix(matrix);

            try
            {
                double determinant = m.CalcDeterminant();
                Assert.Fail("Expected InvalidOperationException for non-square matrix.");
            }
            catch (InvalidOperationException)
            {
                // Expected exception
            }
        }
    }
}

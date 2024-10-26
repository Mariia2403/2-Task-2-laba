using System;

namespace _2_лаба
{
    partial class MyMatrix
    {
        public static MyMatrix operator +(MyMatrix m1, MyMatrix m2)
        {
            if (m1.GetHeight() != m2.GetHeight() || m1.GetWidth() != m2.GetWidth())
            {
                throw new ArgumentException("Матриці повинні мати однаковий розмір для додавання.");
            }

            double[,] mRes = new double[m1.GetHeight(), m2.GetWidth()];
            for (int i = 0; i < m1.GetHeight(); i++)
            {
                for (int j = 0; j < m2.GetWidth(); j++)
                {
                    mRes[i, j] = m1[i, j] + m2[i, j];
                }
            }

            return new MyMatrix(mRes);
        }
        public static MyMatrix operator *(MyMatrix m1, MyMatrix m2)
        {
            int RowsCount = m1.GetHeight();
            int ColsCount = m2.GetWidth();
            if (m1.GetWidth() != m2.GetHeight())
            {
                throw new ArgumentException("Для множення кількість стовпців першої матриці повинна дорівнювати кількості рядків другої матриці.");
            }

            double[,] mRes = new double[m1.GetHeight(), m2.GetWidth()];

            for (int i = 0; i < m1.GetHeight(); i++)
            {
                for (int j = 0; j < m2.GetWidth(); j++)
                {
                    mRes[i, j] = 0;
                    for (int k = 0; k < m1.GetWidth(); k++) // Використовуємо стовпці першої матриці
                                                            // та рядки другої
                    {
                        mRes[i, j] += m1[i, k] * m2[k, j];
                    }
                }
            }

            return new MyMatrix(mRes);
        }

        private double[,] GetTransponedArray(double[,] a)
        {
            MyMatrix m = new MyMatrix();
            m.matrix = new double[a.GetLength(1), a.GetLength(0)];

            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    m.matrix[j, i] = a[i, j]; 
                }
            }
            return m.matrix;
        }

        public MyMatrix GetTransponedCopy(MyMatrix m1)
        {
            int rows = m1.matrix.GetLength(0);
            int cols = m1.matrix.GetLength(1);

            double[,] newMatrix = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    newMatrix[i, j] = m1.matrix[i, j];
                }
            }

            // GetTransponedArray(newMatrix);
            MyMatrix m2 = new MyMatrix(GetTransponedArray(newMatrix));
            //Console.WriteLine(m2);
            return m2;
        }

        public MyMatrix TransponedMe()
        {
            matrix = GetTransponedArray(matrix); // Це має бути правильно.
                                                 // Щоб вивести матрицю, вам може знадобитися реалізувати метод для форматування виходу
            return this; // Повернути поточний екземпляр
        }

        public double CalcDeterminant()
        {
            // Якщо детермінант вже обчислено і матриця не змінювалася,
            // повертає кешоване значення

            if (cachedDeterminant.HasValue)
            {
                return cachedDeterminant.Value;
            }

            if (GetHeight() != GetWidth())
            {
                //такий вид тому що ми працюєм з об'єктом
                //і він нам не підходить
                throw new InvalidOperationException("Детермінант можна обчислити лише для квадратних матриць.");
            }
            double[,] matrixCopy = (double[,])matrix.Clone();

            double determinant = CalculateDeterminant();

            cachedDeterminant = determinant;

            return determinant;

        }

        private double CalculateDeterminant()
        {
            double[,] a = (double[,])matrix.Clone();
            int n = a.GetLength(0);
            int i, j, k;
            double det = 1;

            for (i = 0; i < n - 1; i++)
            {
                if (a[i, i] == 0)
                {
                    bool foundNonZero = false;
                    for (int row = i + 1; row < n; row++)
                    {
                        if (a[row, i] != 0)
                        {
                            for (int col = 0; col < n; col++)
                            {
                                double temp = a[i, col];
                                a[i, col] = a[row, col];
                                a[row, col] = temp;
                            }
                            foundNonZero = true;
                            break;
                        }
                    }

                    if (!foundNonZero)
                        return 0;
                }

                for (j = i + 1; j < n; j++)
                {
                    double multiplier = a[j, i] / a[i, i];
                    for (k = i; k < n; k++)
                        a[j, k] = a[j, k] - multiplier * a[i, k];
                }
            }

            for (i = 0; i < n; i++)
                det *= a[i, i];

            return det;
        }



    }

}

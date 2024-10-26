using System;

namespace _2_лаба
{
    partial class MyMatrix
    {
        int height;
        int width;

       
        public int Height
        {
            get
            {
            return height;
            }
            set 
            {
                if (value >= 0)
                { 
                height = value;
                }
            }
        }

        public int Width
        {

            get
            {
                return width;
            }
            set
            {
                if (value >= 0)
                {
                    width = value;
                }
            }
        }

    

        public double GetElement(int h, int w)
        {
            Height = h;
            Width = w;
            if ( h < matrix.GetLength(0) && w < matrix.GetLength(1))
            {
                return matrix[Height, Width];
            }
            else
            {
                throw new ArgumentException("Некоректні дані ");
            }

        }
        public void SetElement(int h, int w, double num)
        {
            Height = h;
            Width = w;
            if ( h< matrix.GetLength(0) && w < matrix.GetLength(1))
            {
                matrix[Height, Width] = num;
            }
            else
            {
                throw new ArgumentException("Некоректні дані ");
            }
        }
        public int GetHeight()
        {
            return matrix.GetLength(0);
        }
        public int GetWidth()
        {
            return matrix.GetLength(1);
        }
        override public String ToString()
        {
            string s = "";
            for (int i = 0; i < GetHeight(); ++i)
            {
                for (int j = 0; j < GetWidth(); ++j)
                {
                    s += matrix[i, j] + " ";
                }
                s += "\n";
            }
            return s.TrimEnd(); // Прибираємо зайвий новий рядок в кінці
        }


        //копіюючий з іншого примірника цього самого класу MyMatrix;
        public MyMatrix(MyMatrix other)
        {
            if (other == null)
            {
                //nameof поверне ім'я змінної
                throw new ArgumentNullException(nameof(other), "Копійований об'єкт не може бути null.");
            }

            int row = other.matrix.GetLength(0);
            int col = other.matrix.GetLength(1);

            this.matrix = new double[row, col];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    this.matrix[i, j] = other.matrix[i, j];
                }
            }
        }


        //з двовимірного масиву типу double[,];
        public MyMatrix(double[,] myMatrix)
        {
            matrix = myMatrix;
        }

        public MyMatrix(double[][] jaggedArr)
        {

            if (jaggedArr == null || jaggedArr.Length == 0)
            {
                throw new System.ArgumentException("Зубчастий масив не може бути порожнім");
            }
            //Перевірка
            bool check = CheckJaggedArr(jaggedArr);
            if (check)
            {
               // int rowCount = jaggedArr.Length;
                Height = jaggedArr.Length;
                //int columnCount = jaggedArr[0].Length;
                Width = jaggedArr[0].Length;
                matrix = new double[height, Width];

                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        matrix[i, j] = jaggedArr[i][j];
                    }
                }

            }

        }


        //з масиву рядків, якщо фактично ці рядки містять записані через пробіли та/або
        //числа, а кількість цих чисел у різних рядках однакова.
        public MyMatrix(string[] stringArr)
        {

            for (int i = 0; i < stringArr.Length; i++)
            {
                if (string.IsNullOrEmpty(stringArr[i]))
                {
                    throw new ArgumentException($"Рядок {i + 1} не може бути порожнім.");
                }
            }

            string[] firstRowElem = stringArr[0].Trim().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            Height = stringArr.Length;
            Width = firstRowElem.Length;

            matrix = new double[Height, Width];

            for (int i = 0; i < Height; i++)
            {
                string[] elements = stringArr[i].Trim().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                if (elements.Length != Width)
                {
                    throw new ArgumentException($"У рядку {i + 1} кількість елементів ({elements.Length}) не відповідає першому рядку ({Width}).");
                }

                for (int j = 0; j < Width; j++)
                {
                    if (!double.TryParse(elements[j], out double number))
                    {
                        throw new FormatException($"Елемент '{elements[j]}' у рядку {i + 1} не є допустимим числом.");
                    }

                    matrix[i, j] = number;
                }
            }


        }
        /*  з рядка, що містить як пробіли та/або табуляції(їх трактувати як роздільники
    чисел у одному рядку матриці), так і символи переведення рядка(їх трактувати
    як роздільники рядків) — якщо фактичні дані того рядка утворюють прямокутну
    числову матрицю; зокрема, щоб цим конструктором можна було створити
    матрицю з рядка, раніше сформованого методом ToString*/
        public MyMatrix(string matrixViaSpace)
        {
            if (string.IsNullOrEmpty(matrixViaSpace))
            {
                throw new ArgumentException("Вхідний рядок не може бути порожнім.");
            }

            // Тут розбиваєм рядок на рядки, прибираючи порожні рядки
            string[] rows = matrixViaSpace.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            if (rows.Length == 0)
            {
                throw new ArgumentException("Рядок не містить жодних рядків матриці.");
            }

            string[] firstRowElements = rows[0].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
          Width = firstRowElements.Length;
            Height= rows.Length;

            matrix = new double[Height, Width];

            for (int i = 0; i < Height; i++)
            {
                string[] elements = rows[i].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                if (elements.Length != Width)
                {
                    throw new ArgumentException($"У рядку {i + 1} кількість елементів ({elements.Length}) не відповідає першому рядку ({Width}).");
                }

                for (int j = 0; j < Width; j++)
                {
                    if (!double.TryParse(elements[j], out double number))
                    {
                        throw new FormatException($"Елемент '{elements[j]}' у рядку {i + 1} не є допустимим числом.");
                    }

                    matrix[i, j] = number;
                }
            }


        }
        public bool CheckJaggedArr(double[][] matrix)
        {
            bool res = true;
            for (int i = 0; i < matrix.Length - 1; i++)
            {

                if (matrix[i].Length == matrix[i + 1].Length)
                {
                    res = true;
                }
                else
                {
                    res = false;
                    break;
                }

            }
            return res;
        }



        public double this[int h, int w]
        {
            get
            {
                Height = h;
                Width = w;
                if ( Height < matrix.GetLength(0) && Width < matrix.GetLength(1))
                {
                    return matrix[Height, Width];
                }
                else
                {
                    throw new ArgumentException("Індекс поза межами матриці.");
                }

            }
            set
            {
                Height = h;
                Width = w;

                if ( Height < matrix.GetLength(0) && Width < matrix.GetLength(1))
                {
                    matrix[Height, Width] = value;
                    cachedDeterminant = null;
                }
                else
                {
                    throw new IndexOutOfRangeException("Індекс поза межами матриці.");

                }
            }

        }


    }
}


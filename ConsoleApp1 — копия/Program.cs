using System;
//запитати
//Коли я працювала і написала Console.WriteLine в мене воно не змінило колір 
//і загалом не показувало помилок
namespace _2_лаба
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyMatrix matrix = IntroductionMethod();
            Console.WriteLine();
            Console.Clear();
            Console.WriteLine(matrix);
            while (matrix == null)
            {

                Console.WriteLine("Повторіть спробу");
                matrix = IntroductionMethod();
            }

            MatrixOperations(matrix);

        }

        static MyMatrix IntroductionMethod()
        {
            Console.WriteLine("(1) Введення через двовимірний масив");
            Console.WriteLine("(2) Введення через зубчастий масив");
            Console.WriteLine("(3) Введення рядків масива через масив рядків ");
            Console.WriteLine("(4) Введення рядків масива через Enter ");
            MyMatrix myMatrix;

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    myMatrix = new MyMatrix(InputArr());
                    return myMatrix;

                case 2:
                    myMatrix = new MyMatrix(InputJaggedArr());
                    return myMatrix;

                case 3:
                    myMatrix = new MyMatrix(InputStringArr());
                    return myMatrix;
                case 4:
                    myMatrix = new MyMatrix(InputViaEnter());
                    return myMatrix;

                default:
                    Console.WriteLine("Некоректний вибір. Введіть 1,2,3 або 4.");
                    return null;

            }

        }

        static void MatrixOperations(MyMatrix matrix)
        {
            string choice = "";

            do
            {
                Console.WriteLine("(1) Додавання двох матриць");
                Console.WriteLine("(2) Множення двох матриць");
                Console.WriteLine("(3) Транспонування матриці через метод GetTransponedCopy");
                Console.WriteLine("(4) Транспонування матриці через метод TransponedMe");
                Console.WriteLine("(5) Знаходження детермінанту");
                Console.WriteLine("'STOP' зупинить програму");
                choice = Console.ReadLine();

                Console.Clear();


                switch (choice)
                {
                    case "1":
                        MyMatrix m = CreateSecondMatrix();
                        MyMatrix mRes = matrix + m;
                        Console.Clear();
                        Console.WriteLine("Перша матриця \n" + matrix.ToString());
                        Console.WriteLine("Друга матриця \n" + m.ToString());
                        Console.WriteLine("Result: \n" + mRes.ToString());
                        break;

                    case "2":
                        MyMatrix m2 = CreateSecondMatrix();
                        Console.Clear();
                        Console.WriteLine("Перша матриця \n" + matrix.ToString());
                        Console.WriteLine("Друга матриця \n" + m2.ToString());
                        MyMatrix mRes2 = matrix * m2;
                        Console.WriteLine("Result: \n" + mRes2.ToString());
                        break;
                    case "3":
                        MyMatrix m3 = new MyMatrix(matrix.GetTransponedCopy(matrix));
                        Console.WriteLine("My matrix:");
                        Console.WriteLine(matrix.ToString());
                        Console.WriteLine("Транспонована матриця:");
                        Console.WriteLine(m3.ToString());
                        break;
                    case "4":
                       matrix =  matrix.TransponedMe();
                        Console.WriteLine("Result:");
                        Console.WriteLine(matrix);

                        break;
                    case "5":
                        double det = matrix.CalcDeterminant();
                        Console.WriteLine("Матриця: ");
                        Console.WriteLine(matrix);
                        Console.WriteLine("Детермінант: " + det);
                        break;

                    default:

                        Console.WriteLine("Некоректний вибір. Введіть 1, 2,3,4 або 5.");
                        break;
                }
                Choice(matrix);
            } while (choice != "STOP");
        }

        static void Choice(MyMatrix matrix)
        {
            string choice_2 = " ";
            do
            {
                Console.WriteLine("Перевірити роботу методів 'YES' / 'NO'");
                choice_2 = Console.ReadLine();

                if (choice_2 == "YES")
                {
                    CheckMethods(matrix);
                    Console.WriteLine("Hi");
                    MatrixOperations(matrix);
                }
                else if (choice_2 == "NO")
                {
                    MatrixOperations(matrix);
                }
                else
                {
                    Console.WriteLine("Try again");
                }
            } while (true);


        }
        static void CheckMethods(MyMatrix m)
        {

            Console.WriteLine("Перевірка роботи SetElement");
            Console.WriteLine("Row");
            int h = int.Parse(Console.ReadLine());
            Console.WriteLine("Col");
            int w = int.Parse(Console.ReadLine());
            Console.WriteLine("Num");
            double num = double.Parse(Console.ReadLine());
            Console.WriteLine("Result:");
            m.SetElement(h - 1, w - 1, num);
            Console.WriteLine(m);

            Console.WriteLine("Перевірка роботи GetElement");
            Console.WriteLine("Row");
            int h_ = int.Parse(Console.ReadLine());
            Console.WriteLine("Col");
            int w_ = int.Parse(Console.ReadLine());
            Console.WriteLine("Result:");
            Console.WriteLine(m.GetElement(h_ - 1, w_ - 1));

            Console.WriteLine("Перевірка роботи індексатора");
            Console.WriteLine("Row");
            int h1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Col");
            int w1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Result");
            Console.WriteLine(m[h1 - 1, w1 - 1]);
            Console.WriteLine("Введіть нове число");
            double newNum = double.Parse(Console.ReadLine());
            m[h1 - 1, w1 - 1] = newNum;
            Console.WriteLine(m);

        }



        static MyMatrix CreateSecondMatrix()
        {

            MyMatrix newM = new MyMatrix();
            newM = IntroductionMethod();
            return newM;
        }
        static double[,] InputArr()
        {

            Console.WriteLine("Введіть кількість рядків у матриці:");
            int row = int.Parse(Console.ReadLine());

            Console.WriteLine("Введіть кількість стовпчиків у матриці:");
            int col = int.Parse(Console.ReadLine());

            double[,] matrix = new double[row, col];
            Console.WriteLine("Введіть матрицю");
            for (int i = 0; i < row; i++)
            {
                string[] input = Console.ReadLine().Split(' ');
                for (int j = 0; j < col; j++)
                {
                    matrix[i, j] = Convert.ToDouble(input[j]);
                }
            }
            return matrix;
        }

        static double[][] InputJaggedArr()
        {
            Console.WriteLine("Введіть кількість рядків у зубчастому масиві:");
            int numberOfRows = int.Parse(Console.ReadLine());

            double[][] jaggedArr = new double[numberOfRows][];
            Console.WriteLine($"Введіть кількість елементів для рядків :");
            int numberOfElements = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfRows; i++)
            {
                jaggedArr[i] = new double[numberOfElements];

                string[] elements = Console.ReadLine().Split(' ');

                for (int j = 0; j < numberOfElements; j++)
                {

                    if (j < elements.Length && double.TryParse(elements[j], out double element))
                    {
                        jaggedArr[i][j] = element;
                    }
                    else
                    {
                        Console.WriteLine("Некоректний ввід, спробуйте ще раз.");
                        j--;
                    }
                }
            }
            return jaggedArr;
        }

        static string[] InputStringArr()
        {
            Console.WriteLine("Введіть кількість рядків у матриці:");
            int rows = int.Parse(Console.ReadLine());

            string[] stringArr = new string[rows];

            for (int i = 0; i < rows; i++)
            {
                stringArr[i] = Console.ReadLine();
            }
            return stringArr;
        }

        static string InputViaEnter()
        {
            string input = string.Empty;
            string line;
            Console.WriteLine("Введіть матрицю (порожній рядок для завершення):");

            while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
            {
                input += line + Environment.NewLine; // додаємо введений рядок до вхідних даних
            }

            return input; // повертаємо введений текст як один рядок


        }
    }
}




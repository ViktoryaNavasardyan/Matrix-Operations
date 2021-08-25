using System;

namespace Matrix_Operations
{
    struct Matrix
    {
        int rows;
        int columns;
        public double[,] Array;
        public int GetRows() => rows;
        public int GetColumns() => columns;
        public Matrix(int a, int b)
        {
            rows = a;
            columns = b;
            Array = new double[a, b];
        }
        public void Input(int row, int column)
        {
            bool flag = true;
            for (int i = 0; i < row; i++)
            {
                Console.WriteLine("Input matrix rows separated by space. And then press enter.");
                string[] substring;
                do
                {
                    flag = true;
                    string rows = Console.ReadLine();
                    substring = rows.Split(' ');
                    if (substring.Length != column)
                    {
                        Console.WriteLine("Your input number is not equal to number of columns." +
                            "Try again");
                        flag = false;
                    } 
                    if (flag)
                    {
                        
                        for(int j = 0; j < column; j++)
                        {

                            flag = double.TryParse(substring[j], out Array[i,j]);
                            if (flag == false)
                            {
                                Console.WriteLine("Your input is not number. Try again");
                                break;
                            }
                        }
                    }
                } while (!flag);
            }
        }
        public bool Addition(Matrix m1, Matrix m2)
        {
            if (m1.rows == m2.rows && m1.columns == m2.columns)
            {
                for (int i = 0; i < rows; i++)
                    for (int j = 0; j < columns; j++)
                        Array[i, j] = m1.Array[i, j] + m2.Array[i, j];
                return true;
            }
            else
            {
                Console.WriteLine("Incorrect matrixes.");
                return false;
            }
        } //done
        public bool Multiplication(Matrix m1, Matrix m2)
        {
            if (m1.columns == m2.rows)
            {
                double sum0 = 0; 
                int l = 0;
                int n = m1.rows;
                int p = m2.columns;
                int m = m2.rows;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; l < p; l++)
                    {
                        for (int r = 0; r < m; r++)
                        {
                            sum0 += m2.Array[j, l] * m1.Array[i, j++];
                        }
                        Array[i, l] = sum0;
                        sum0 = 0;
                        j = 0;
                    }
                    l = 0;
                }
                return true;
            }
            else
            {
                Console.WriteLine("Incorrect matrixes.");
                return false;
            }
        } //done
        public void ScalarMultiplication(int scalar, Matrix m)
        {
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    Array[i, j] = m.Array[i, j] * scalar;
        } //done
        public bool IsOrthogonal()
        {
            if (rows == columns)
            {
                int n = rows;
                Matrix identity = new Matrix(n, n);
                for (int i = 0; i < n; i++)
                {
                    identity.Array[i, i] = 1;
                }
                Matrix transpseMatrix = new Matrix(n, n);
                transpseMatrix.Transpose(this);
                Matrix multiMatrix = new Matrix(n, n);
                multiMatrix.Multiplication(this, transpseMatrix);
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (multiMatrix.Array[i, j] != identity.Array[i, j])
                            return false;
                    }
                }
                return true;
            }
            return false;
        }
        /*public Matrix Inverse()
        {

        }*/
        public double DET(int n, double[,] Mat, int m)
        {
            if (n == m)
            {
                double d = 0;
                int k, i, j, subi, subj;
                double[,] SUBMat = new double[n, n];

                if (n == 2)
                {
                    return ((Mat[0, 0] * Mat[1, 1]) - (Mat[1, 0] * Mat[0, 1]));
                }

                else
                {
                    for (k = 0; k < n; k++)
                    {
                        subi = 0;
                        for (i = 1; i < n; i++)
                        {
                            subj = 0;
                            for (j = 0; j < n; j++)
                            {
                                if (j == k)
                                {
                                    continue;
                                }
                                SUBMat[subi, subj] = Mat[i, j];
                                subj++;
                            }
                            subi++;
                        }
                        d = d + (Math.Pow(-1, k) * Mat[0, k] * DET(n - 1, SUBMat, n - 1));
                    }
                }
                return d;
            }
            else
            {
                Console.WriteLine("Incorrect matrixes.");
                return 0;
            }
        
        }
        public void Transpose(Matrix m)
        {
            for (int i = 0; i < m.rows; i++)
                for (int j = 0; j < m.columns; j++)
                {
                    Array[j, i] = m.Array[i, j];
                }
        } //done
        public double MaxValue()
        {
            double max = Array[0, 0];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    if (Array[i, j] > max)
                        max = Array[i, j];
            return max;
        } //done
        public double MinValue()
        {
            double min = Array[0, 0];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    if (Array[i, j] < min)
                        min = Array[i, j];
            return min;
        } //done
        public void Print()
        {
           // Console.WriteLine("Your Matrix is : ");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                    Console.Write(Array[i, j] + "\t");
                Console.WriteLine();
            }
            Console.WriteLine();
        } //done

    }
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            Console.WriteLine("Matrix Operations");
            Console.WriteLine("------------------------\n");
            while (!endApp)
            {
                Matrix First = InputFirstMatrix();
                string operation = EnterOperation();
                Matrix Second = default;
                int scalar = default;
                switch (operation)
                {
                    case "Addition":
                    case "Multiplication":
                        Second = InputSecondMatrix();
                        break;
                    case "ScalarMultiplication":
                        Console.WriteLine("Enter the number, please integer.");
                        scalar = Convert.ToInt32(Console.ReadLine());
                        break;
                }
                Console.WriteLine("The result is: ");
                switch (operation)
                {
                    case "Addition":
                        Matrix Added = new Matrix(First.GetRows(),First.GetColumns());
                        bool res = Added.Addition(First, Second);
                        if(res)
                            PrintResult("Addition", First, Second, Added);
                        break;
                    case "Multiplication":
                        Matrix Multiplied = new Matrix(First.GetRows(), Second.GetColumns()); ;
                        res = Multiplied.Multiplication(First, Second);
                        if (res)
                            PrintResult("Addition", First, Second, Multiplied);
                        break;
                    case "ScalarMultiplication":
                        Matrix ScMultiplied = new Matrix(First.GetRows(), First.GetColumns());
                        ScMultiplied.ScalarMultiplication(scalar, First);
                        PrintResult(scalar, First, ScMultiplied);
                        break;
                    case "Transpose":
                        Matrix Transposed = new Matrix(First.GetColumns(), First.GetRows());
                        Transposed.Transpose(First);
                        PrintResult("Transpose", Transposed);
                        break;
                    case "IsOrthogonal":
                        PrintResult("IsOrthogonal", First);
                        break;
                    case "MinValue":
                        PrintResult("MinValue", First);
                        break;
                    case "MaxValue":
                        PrintResult("MaxValue", First);
                        break;
                }
                #region Matrixes
                /* Matrix myMatrix = new Matrix(5,3);
                 Matrix myMatrix2 = new Matrix(3,4);
                 myMatrix.Array[0, 0] = 1;
                 myMatrix.Array[0, 1] = 0;
                 myMatrix.Array[0, 2] = -2;
                 myMatrix.Array[1, 0] = 0;
                 myMatrix.Array[1, 1] = 3;
                 myMatrix.Array[1, 2] = -1;
                 myMatrix.Array[2, 0] = 8;
                 myMatrix.Array[2, 1] = 1;
                 myMatrix.Array[2, 2] = 8;
                 myMatrix.Array[3, 0] = 1;
                 myMatrix.Array[3, 1] = 8;
                 myMatrix.Array[3, 2] = 1;
                 myMatrix.Array[4, 0] = 8;
                 myMatrix.Array[4, 1] = 1;
                 myMatrix.Array[4, 2] = 1;


                 myMatrix2.Array[0, 0] = 0;
                 myMatrix2.Array[0, 1] = 3;
                 myMatrix2.Array[0, 2] = 8;
                 myMatrix2.Array[0, 3] = 1;
                 myMatrix2.Array[1, 0] = -2;
                 myMatrix2.Array[1, 1] = -1;
                 myMatrix2.Array[1, 2] = 8;
                 myMatrix2.Array[1, 3] = 1;
                 myMatrix2.Array[2, 0] = 0;
                 myMatrix2.Array[2, 1] = 4;
                 myMatrix2.Array[2, 2] = 8;
                 myMatrix2.Array[2, 3] = 1;

                 myMatrix.Print();
                 myMatrix2.Print();*/
                #endregion
                #region End Process
                Console.WriteLine("If you want to end process press 'e'. If not, press enter and continue.");
                string key = Console.ReadLine();
                if (Equals(key, "e"))
                {
                    endApp = true;
                }
                #endregion
            }
        }
        static Matrix InputFirstMatrix()
        {
            int row1 = In("row_1");
            int column1 = In("column_1");
            Matrix FirstMatrix = new Matrix(row1, column1);
            FirstMatrix.Input(row1, column1);
            Console.WriteLine("......First Matrix......");
            FirstMatrix.Print();
            return FirstMatrix;
        }
        static Matrix InputSecondMatrix()
        {
            int row2 = In("row_2");
            int column2 = In("column_2");
            Matrix SecondMatrix = new Matrix(row2, column2);
            SecondMatrix.Input(row2, column2);
            Console.WriteLine("......Second Matrix......");
            SecondMatrix.Print();
            return SecondMatrix;
        }
        static int In(string s)
        {
            string number_string;
            int number = 0;
            Console.WriteLine($"Input matrix {s} number.");
            number_string = Console.ReadLine();
            while (!int.TryParse(number_string, out number))
            {
                Console.WriteLine("Not a valid number, try again.");

                number_string = Console.ReadLine();
            }
            return number;
        }
        static string EnterOperation()
        {
            Console.WriteLine(@"Select operation:
                                             Addition
                                             Multiplication
                                             ScalarMultiplication
                                             Transpose
                                             IsOrthogonal
                                             MaxValue
                                             MinValue");
            string operation = Console.ReadLine();
            while (operation != "Addition" && operation != "Multiplication" && operation != "ScalarMultiplication" && operation != "Transpose"
                && operation != "IsOrthogonal" && operation != "MaxValue" && operation != "MinValue")
            {
                Console.WriteLine("This is not an operation.Try again.");
                operation = Console.ReadLine();
            }
            return operation;
        }
        static void PrintResult(string operation, Matrix Matrix1, Matrix Matrix2, Matrix ResultMatrix)
        {
            switch (operation)
            {
                case "Addition":
                    Matrix1.Print();
                    Console.WriteLine("+");
                    Console.WriteLine();
                    Matrix2.Print();
                    Console.WriteLine("____________=");
                    ResultMatrix.Print();
                    break;
                case "Multiplication":
                    Matrix1.Print();
                    Console.WriteLine("*");
                    Console.WriteLine();
                    Matrix2.Print();
                    Console.WriteLine("____________=");
                    ResultMatrix.Print();
                    break;
            }
        }
        //method overloading :)
        static void PrintResult(string operation, Matrix Matrix)
        {
            switch (operation)
            {
                case "Transpose":
                    Console.WriteLine("Transposed matrix: ");
                    Matrix.Print();
                    break;
                case "IsOrthogonal":
                    if (Matrix.IsOrthogonal())
                        Console.WriteLine("The matrix is orthogonal.");
                    else Console.WriteLine("The matrix is not orthogonal.");
                    break;
                case "MinValue":
                    Console.WriteLine($"Min Value is: {Matrix.MinValue()}");
                    break;
                case "MaxValue":
                    Console.WriteLine($"Max Value is: {Matrix.MaxValue()}");
                    break;
            }
        }
        static void PrintResult(int number, Matrix Matrix, Matrix ResultMatrix)
        {
             Console.WriteLine($"{number}");
            Console.WriteLine("*");
            Matrix.Print();
            Console.WriteLine("____________=");
            ResultMatrix.Print();
        }
    }
}

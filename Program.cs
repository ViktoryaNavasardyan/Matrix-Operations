using System;

namespace Matrix_Operations
{
    //Implement Matrix Calculations
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
        }
        public bool Multiplication(Matrix m1, Matrix m2)
        {
            if (m1.columns == m2.rows)
            {
                double sum = 0;
                int m = m1.rows;
                int p = m2.columns;
                int n = m2.rows;
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < p; j++)
                    {
                        for (int k = 0; k < n; k++)
                        {
                            Array[i, j] += m1.Array[i, k] * m2.Array[k, j];
                        }
                        sum = 0;
                    }
                }
                return true;
            }
            else
            {
                Console.WriteLine("Incorrect matrixes.");
                return false;
            }
        } 
        public void ScalarMultiplication(double scalar, Matrix m)
        {
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    Array[i, j] = m.Array[i, j] * scalar;
        }
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
        public bool Inverse(Matrix matrix)
        {
            if (matrix.Determinant(matrix.rows, matrix.Array, matrix.columns) == 0)
            {
                Console.WriteLine("Determinant of this Matrix = 0. Therefore this Matrix can not be inversed.");
                return false;
            }
            if(matrix.rows!=matrix.columns)
            {
                Console.WriteLine("This Matrix is not square. Therefore this Matrix can not be inversed.");
                return false;
            }
            if(matrix.rows == 2)
            {
                Matrix Temp = new Matrix(matrix.rows, matrix.columns);
                Temp.Array[0,0] = matrix.Array[1,1]; Temp.Array[0,1] = (-1) * matrix.Array[0,1];
                Temp.Array[1,0] = (-1) * matrix.Array[1,0]; Temp.Array[1,1] = matrix.Array[0,0];
                ScalarMultiplication(1/matrix.Determinant(matrix.rows, matrix.Array, matrix.columns), Temp);
                return true;
            }
            if (matrix.rows == 3)
            {
                Console.WriteLine($"determinant : {matrix.Determinant(3,matrix.Array,3)}.");
                Matrix minor = new Matrix(3,3);
                minor.Array[0, 0] = (matrix.Array[1, 1] * matrix.Array[2, 2]) - (matrix.Array[1, 2] * matrix.Array[2, 1]);
                minor.Array[0, 1] = (matrix.Array[1, 0] * matrix.Array[2, 2]) - (matrix.Array[1, 2] * matrix.Array[2, 0]);
                minor.Array[0, 2] = (matrix.Array[1, 0] * matrix.Array[2, 1]) - (matrix.Array[1, 1] * matrix.Array[2, 0]);

                minor.Array[1, 0] = (matrix.Array[0, 1] * matrix.Array[2, 2]) - (matrix.Array[0, 2] * matrix.Array[2, 1]);
                minor.Array[1, 1] = (matrix.Array[0, 0] * matrix.Array[2, 2]) - (matrix.Array[0, 2] * matrix.Array[2, 0]);
                minor.Array[1, 2] = (matrix.Array[0, 0] * matrix.Array[2, 1]) - (matrix.Array[0, 1] * matrix.Array[2, 0]);

                minor.Array[2, 0] = (matrix.Array[0, 1] * matrix.Array[1, 2]) - (matrix.Array[0, 2] * matrix.Array[1, 1]);
                minor.Array[2, 1] = (matrix.Array[0, 0] * matrix.Array[1, 2]) - (matrix.Array[0, 2] * matrix.Array[1, 0]);
                minor.Array[2, 2] = (matrix.Array[0, 0] * matrix.Array[1, 1]) - (matrix.Array[0, 1] * matrix.Array[1, 0]);

                Matrix cofactor = new Matrix(3, 3);
                cofactor.Array[0, 0] = minor.Array[0, 0]; cofactor.Array[0, 1] = -1 * minor.Array[0, 1]; cofactor.Array[0, 2] = minor.Array[0, 2];
                cofactor.Array[1, 0] = -1 * minor.Array[1, 0]; cofactor.Array[1, 1] = minor.Array[1, 1]; cofactor.Array[1, 2] = -1 * minor.Array[1, 2];
                cofactor.Array[2, 0] = minor.Array[2, 0]; cofactor.Array[2, 1] = -1 * minor.Array[2, 1]; cofactor.Array[2, 2] = minor.Array[2, 2];
              
                Matrix AdjC = new Matrix(3, 3);
                AdjC.Transpose(cofactor);
           
                ScalarMultiplication(1 / matrix.Determinant(3, matrix.Array, 3), AdjC);
                return true;
            }
            if (matrix.rows > 3)
            {
                Console.WriteLine("Vika can't calculate the inverse matrix of matrixes that have more than 3 sides. Sorry");
            }
            return false;
        }
        public double Determinant(int n, double[,] Mat, int m)
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
                        d = d + (Math.Pow(-1, k) * Mat[0, k] * Determinant(n - 1, SUBMat, m - 1));
                    }
                }
                return d;
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
        } 
        public double MinValue()
        {
            double min = Array[0, 0];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    if (Array[i, j] < min)
                        min = Array[i, j];
            return min;
        } 
        public void Print()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                    Console.Write(Array[i, j] + "\t");
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public void PrintResult(string operation, Matrix Matrix1, Matrix Matrix2)
        {
            switch (operation)
            {
                case "Addition":
                    Matrix1.Print();
                    Console.WriteLine("+");
                    Console.WriteLine();
                    Matrix2.Print();
                    Console.WriteLine("____________=");
                    Print();
                    break;
                case "Multiplication":
                    Matrix1.Print();
                    Console.WriteLine("*");
                    Console.WriteLine();
                    Matrix2.Print();
                    Console.WriteLine("____________=");
                    Print();
                    break;
            }
        }

        public void PrintResult(string operation)
        {
            switch (operation)
            {
                case "Transpose":
                    Console.WriteLine("Transposed matrix: ");
                    Print();
                    break;
                case "IsOrthogonal":
                    if (IsOrthogonal())
                        Console.WriteLine("The matrix is orthogonal.");
                    else Console.WriteLine("The matrix is not orthogonal.");
                    break;
                case "MinValue":
                    Console.WriteLine($"Min Value is: {MinValue()}");
                    break;
                case "MaxValue":
                    Console.WriteLine($"Max Value is: {MaxValue()}");
                    break;
                case "Inverse":
                    Console.WriteLine("Inversed matrix: ");
                    Print();
                    break;
            }
        }
        public void PrintResult(int number, Matrix Matrix)
        {
            Console.WriteLine($"{number}");
            Console.WriteLine("*");
            Matrix.Print();
            Console.WriteLine("____________=");
            Print();
        }

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
                            Added.PrintResult("Addition", First, Second);
                        break;
                    case "Multiplication":
                        Matrix Multiplied = new Matrix(First.GetRows(), Second.GetColumns()); ;
                        res = Multiplied.Multiplication(First, Second);
                        if (res)
                            Multiplied.PrintResult("Addition", First, Second);
                        break;
                    case "ScalarMultiplication":
                        Matrix ScMultiplied = new Matrix(First.GetRows(), First.GetColumns());
                        ScMultiplied.ScalarMultiplication(scalar, First);
                        ScMultiplied.PrintResult(scalar, First);
                        break;
                    case "Transpose":
                        Matrix Transposed = new Matrix(First.GetColumns(), First.GetRows());
                        Transposed.Transpose(First);
                        Transposed.PrintResult("Transpose");
                        break;
                    case "IsOrthogonal":
                        First.PrintResult("IsOrthogonal");
                        break;
                    case "MinValue":
                        First.PrintResult("MinValue");
                        break;
                    case "MaxValue":
                        First.PrintResult("MaxValue");
                        break;
                    case "Determinant":
                        if (CheckForDet(First))
                        {
                            Console.WriteLine($"Determinant = {First.Determinant(First.GetRows(), First.Array, First.GetColumns())}");
                        }
                        else Console.WriteLine($"Incorrect Matrix.");
                        break;
                    case "Inverse":
                        Matrix Inversed = new Matrix(First.GetColumns(), First.GetRows());
                        res = Inversed.Inverse(First);
                        if (res)
                        {
                            Inversed.PrintResult("Inverse");
                        }
                        break;
                }
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
        static bool CheckForDet(Matrix m) => m.GetColumns() == m.GetRows();
        static Matrix InputFirstMatrix()
        {
            int row1 = InputSides("1 row");
            int column1 = InputSides("1 column");
            Matrix FirstMatrix = new Matrix(row1, column1);
            FirstMatrix.Input(row1, column1);
            Console.WriteLine("......First Matrix......");
            FirstMatrix.Print();
            return FirstMatrix;
        }
        static Matrix InputSecondMatrix()
        {
            int row2 = InputSides("2 row");
            int column2 = InputSides("2 column");
            Matrix SecondMatrix = new Matrix(row2, column2);
            SecondMatrix.Input(row2, column2);
            Console.WriteLine("......Second Matrix......");
            SecondMatrix.Print();
            return SecondMatrix;
        }
        static int InputSides(string s)
        {
            string number_string;
            int number = 0;
            Console.WriteLine($"Input matrix {s} number.");
            number_string = Console.ReadLine();
             
            while (!int.TryParse(number_string, out number) || Convert.ToInt32(number_string) < 1)
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
                                             MinValue
                                             Determinant
                                             Inverse");
            string operation = Console.ReadLine();
            while (operation != "Addition" && operation != "Multiplication" && operation != "ScalarMultiplication" && operation != "Transpose"
                && operation != "IsOrthogonal" && operation != "MaxValue" && operation != "MinValue" && operation != "Determinant" && operation != "Inverse")
            {
                Console.WriteLine("This is not supported operation.Try again.");
                operation = Console.ReadLine();
            }
            return operation;
        }
        //method overloading :)
        

    }
}

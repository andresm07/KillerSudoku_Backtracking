using KillerSudoku.GameBoard.Logic.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerSudoku.GameBoard.Logic.Board
{
    public class Generator
    {
        public static int generateResult(Shape.ShapeTypeID shape, Operation operation, int range)
        {
            int[] array = generateRandomArray(range, shape.getLength(), operation);
            int attempts = 1000;
            while (!isValid(array, shape.getValidCombination()) && attempts-- > 0)
            {
                array = generateRandomArray(range, shape.getLength(), operation);
            }
            return operate(operation, array);
        }

        public static int operate(Operation op, int[] array)
        {
            if(op.Equals(Operation.ADD))
            {
                return sum(array);
            }
            else if(op.Equals(Operation.SUB))
            {
                return sub(array);
            }
            else if(op.Equals(Operation.MOD))
            {
                return mod(array);
            }
            else if(op.Equals(Operation.EXP))
            {
                return exp(array);
            }
            else if(op.Equals(Operation.MUL))
            {
                return mul(array);
            }
            else
            {
                return div(array);
            }
        }

        public static int calculateNumFontSize(int x)
        {
            return (int)(160 * (Math.Pow(0.87055056, x)));
        }

        public static int calculateFontSize(int x)
        {
            return (int)(-0.64 * x + 21.09) > 0 ? (int)(-0.64 * x + 21.09) : 1;
        }

        private static int sum(int[] array)
        {
            int result = 0;
            foreach (int i in array)
            {
                result += i;
            }
            return result;
        }

        private static int exp(int[] array)
        {
            return (int)Math.Pow(array[0], 3);
        }

        static int mul(int[] array)
        {
            int result = 1;
            foreach (int i in array)
            {
                result *= i;
            }
            return result;
        }

        private static int sub(int[] array)
        {
            int result = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                result -= array[i];
            }
            return result;
        }

        private static int mod(int[] array)
        {
            return array[1] == 0 ? array[0] % 3 : array[0] % array[1];
        }

        private static int div(int[] array)
        {
            return array[1] == 0 ? array[0] / (array[0] + 2) : array[0] / array[1];
        }

        private static int[] generateRandomArray(int pRange, int length, Operation operation)
        {
            Random r = new Random();
            int[] array = new int[length];
            int[] possibilities = range(pRange);
            for (int i = 0; i < length; i++)
            {
                int element = r.Next(pRange);
                element = possibilities[element];
                while (operation.Equals(Operation.MUL) && element == 0)
                {
                    element = r.Next(pRange);
                    element = possibilities[element];
                }
                array[i] = element;
            }
            return array;
        }

        private static Boolean isValid(int[] array, int[][] valid)
        {
            for(int i = 0; i < array.Length; i++)
            {
                if(repeats(array[i],array,i))
                {
                    if(!isIn(i,repetition(array[i],array,i),valid))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static Boolean isIn(int indexA, int indexB, int[][] valid)
        {
            foreach (int[] e in valid)
            {
                if ((e[0] == indexA || e[0] == indexB) && (e[1] == indexA || e[1] == indexB))
                {
                    return true;
                }
            }
            return false;
        }

        private static int repetition(int element, int[] array, int index)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (i == index)
                    continue;
                if (array[i] == element)
                    return i;
            }
            return -1;
        }

        private static Boolean repeats(int element, int[] array, int index)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (i == index)
                    continue;
                if (array[i] == element)
                    return true;
            }
            return false;
        }

        public static int[] range(int n)
        {
            int[] possibilities = new int[n];
            for (int i = 0; i < n; i++)
            {
                possibilities[i] = i;
            }
            return possibilities;
        }

        public static int[][] generateMatrix(int[][] matrix)
        {
            int[] row = range(matrix.GetLength(0));
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i][j] = row[(j + i) % (matrix.Length)];
                }
            }
            Random r = new Random();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (r.Next(2) == 1)
                    matrix = swapRows(matrix, r.Next(matrix.GetLength(1)), r.Next(matrix.GetLength(1)));
                else
                    matrix = swapColumns(matrix, r.Next(matrix.Length), r.Next(matrix.Length));
            }
            return matrix;
        }

        private static int[][] swapRows(int[][] matrix, int rowA, int rowB)
        {
            int[] rowTemp = matrix[rowA];
            matrix[rowA] = matrix[rowB];
            matrix[rowB] = rowTemp;
            return matrix;
        }

        private static int[][] swapColumns(int[][] matrix, int columnA, int columnB)
        {
            int[] columnTemp = new int[matrix.Length];
            for (int i = 0; i < matrix.Length; i++)
            {
                columnTemp[i] = matrix[i][columnA];
                matrix[i][columnA] = matrix[i][columnB];
                matrix[i][columnB] = columnTemp[i];
            }
            return matrix;
        }

    }
}

using KillerSudoku.GameBoard.Logic.Board;
using KillerSudoku.GameBoard.Logic.Shapes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KillerSudoku.GameSolver
{
    public class Solver
    {
        private Operate permutation;
        private KillerSudokuBoard sudokuBoard;
        private Boolean random;
        private int shapeNum;
        private int[][] board;
        private Shape[][] shapeBoard;

        public Solver(KillerSudokuBoard board, int length)
        {
            this.sudokuBoard = board;
            this.board = this.sudokuBoard.getBoard();
            this.shapeBoard = this.sudokuBoard.getShapeBoard();
            this.permutation = new Operate(generateNumbers(length));
            this.shapeNum = countShapes(board.getShapeBoard());
            this.random = false;
        }

        public Solver(KillerSudokuBoard board, int length, Boolean random)
        {
            this.sudokuBoard = board;
            this.random = random;
            this.board = this.sudokuBoard.getBoard();
            this.shapeBoard = this.sudokuBoard.getShapeBoard();
            this.permutation = new Operate(generateNumbers(length));
            this.shapeNum = countShapes(board.getShapeBoard());
        }

        public int[][] getBoard()
        {
            return this.board;
        }

        public Shape[][] getShapeBoard()
        {
            return this.shapeBoard;
        }

        private List<int> generateNumbers(int length)
        {
            List<int> numArray = new List<int>();
            for (int i = 0; i < length; i++)
            {
                numArray.Add(i);
            }
            return numArray;
        }

        private int countShapes(Shape[][] shape)
        {
            int result = 0;
            for (int i = 0; i < shape.Length; i++)
            {
                for (int j = 0; j < shape[0].Length; j++)
                {
                    if (!shape[i][j].visited)
                    {
                        result++;
                        shape[i][j].visited = true;
                    }
                }
            }
            setVisitedFalse(shape);
            return result;
        }

        private int[] getColumn(int[][] matrix, int column)
        {
            int[] result = new int[matrix.Length];
            for (int i = 0; i < matrix.Length; i++)
            {
                result[i] = matrix[i][column];
            }
            return result;
        }

        public List<int[]> generatePermutations(Shape[][] matrix)
        {
            List<int[]> permutations = new List<int[]>();
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[0].Length; j++)
                {
                    Shape shape = matrix[i][j];
                    if (!shape.visited)
                    {
                        String op = shape.getOperation().getSymbol();
                        int length = shape.getID().getLength();
                        int target = shape.getObjective();
                        switch (length)
                        {
                            case 1:
                                permutations = this.permutation.permutateOne(target);
                                break;
                            default:
                                permutations = this.permutation.permut(length, target, op, shape);
                                break;
                        }
                        if (random)
                        {
                            randomize(permutations);
                        }
                        shape.permutations = permutations;
                        shape.visited = true;
                        Console.WriteLine(permutations.Count());
                    }
                }
            }
            setVisitedFalse(matrix);
            return permutations;
        }

        private void printMatrix(int[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[0].Length; j++)
                {
                    Console.WriteLine(matrix[i][j] + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private void randomize(List<int[]> permutations)
        {
            Random random = new Random();
            int rand;
            for (int i = 0; i < permutations.Count(); i++)
            {
                rand = random.Next(permutations.Count);
                int[] array = permutations.ElementAt(0);
                permutations.RemoveAt(0);
                permutations.Insert(rand, array);
            }
        }

        private Boolean solveAux(int row, int column, int[][] matrix, Shape[][] shapeBoard, int counter)
        {
            lock(this)
            {
                //interfaz
            }
            if(complete(counter,(int[][])matrix.Clone()))
            {
                return true;
            }
            int size = matrix.Length;
            Shape shape = this.shapeBoard[row][column];
            if(!(matrix[row][column] == int.MinValue))
            {
                return solveAux(column == size - 1 ? (row % size) + 1 : (row % size), column == size - 1 ? 0 : (column % size) + 1, matrix, shapeBoard, counter);
            }

            List<int[]> permutations = shape.permutations;
            foreach(int[] p in permutations)
            {
                shape.number = (int[])p.Clone();
                if(valid((int[][])shape.setPermutation(matrix).Clone()))
                {
                    matrix = shape.setPermutation(matrix);
                    if(solveAux(column == size - 1 ? (row % size) + 1 : (row % size), column == size - 1 ? 0 : (column % size) + 1, matrix, shapeBoard, counter + 1))
                    {
                        return true;
                    }
                }
                shape.number = empty(shape.number);
                matrix = shape.setPermutation(matrix);
            }
            return false;
        }

        private int[] empty(int[] array)
        {
            for(int i = 0; i < array.Length; i++)
            {
                array[i] = int.MinValue;
            }
            return (int[])array.Clone();
        }

        public Boolean valid(int[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                if(!checkColumn(matrix[i]) || !checkColumn(getColumn(matrix,i)))
                {
                    return false;
                }
            }
            return true;
        }

        private Boolean checkColumn(int[] matrix)
        {
            int cont = 0;
            for(int i = 0; i < matrix.Length; i++)
            {
                for(int j = 0; j < matrix.Length; j++)
                {
                    if(matrix[j] >= 0)
                    {
                        if(i == matrix[j])
                        {
                            cont++;
                        }
                    }
                    if(cont > 1)
                    {
                        return false;
                    }
                }
                cont = 0;
            }
            return true;
        }

        private Boolean complete(int cont, int[][] matrix)
        {
            if(cont == this.shapeNum && valid(matrix))
            {
                return true;
            }
            return false;
        }

        public void solve()
        {
            long startTime = Stopwatch.GetTimestamp();
            int pows = solvePows();
            if(solveAux(0,0,this.board, this.shapeBoard,pows))
            {
                long endTime = Stopwatch.GetTimestamp();
                MessageBox.Show("El Killer Sudoku se resolvio en " + ((endTime-startTime)/1000000).ToString());
            }
            else
            {
                long endTime = Stopwatch.GetTimestamp();
                MessageBox.Show("El Killer Sudoku no se pudo resolver");
            }
        }

        private int solvePows()
        {
            int counter = 0;
            for(int i = 0; i < this.sudokuBoard.getShapeBoard().Length; i++)
            {
                for(int j = 0; j < this.sudokuBoard.getShapeBoard()[0].Length; j++)
                {
                    Shape shape = this.sudokuBoard.getShapeBoard()[i][j];
                    if(shape.getOperation().getSymbol().Equals("^"))
                    {
                        shape.number = shape.permutations.ElementAt(0);
                        shape.setPermutation(this.sudokuBoard.getBoard());
                        counter++;
                        shape.visited = true;
                    }
                }
            }
            return counter;
        }

        private void setVisitedFalse(Shape[][] shapes)
        {
            for (int i = 0; i < shapes.Length; i++)
            {
                for (int j = 0; j < shapes[0].Length; j++)
                {
                    Shape shape = shapes[i][j];
                    shape.visited = false;
                }
            } 
       }
    }
}

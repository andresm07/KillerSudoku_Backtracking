using KillerSudoku.GameBoard.Logic.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerSudoku.GameBoard.Logic.Board
{
    public class KillerSudokuBoard
    {
        private int[][] board;
        private Shape[][] shapeBoard;
        private readonly int COLUMNS;
        private readonly int ROWS;

        public KillerSudokuBoard(int rows, int columns, Boolean random)
        {
            this.COLUMNS = columns;
            this.ROWS = rows;
            initializeBoards();
            if (!random)
            {
                board = Generator.generateMatrix(board);
            }
            generateSudokuBoard(random);
            printBoard();
        }

        private void initializeBoards()
        {
            this.board = new int[this.ROWS][];
            for (int i = 0; i < this.board.Length; i++)
            {
                this.board[i] = new int[this.COLUMNS];
            }
            this.shapeBoard = new Shape[this.ROWS][];
            for (int i = 0; i < this.shapeBoard.Length; i++)
            {
                this.shapeBoard[i] = new Shape[this.COLUMNS];
            }
        }

        private void generateSudokuBoard(Boolean random)
        {
            for (int i = 0; i < this.ROWS; i++)
            {
                for (int j = 0; j < this.COLUMNS; j++)
                {
                    if (this.shapeBoard[i][j] != null)
                    {
                        continue;
                    }
                    Shape shape = ShapeFactory.getInstance(this.ROWS, random);
                    while (!shape.fits(this.shapeBoard, i, j, this.board))
                    {
                        shape = ShapeFactory.getInstance(this.ROWS, random);
                    }
                    shape.placeShape(this.shapeBoard, this.board, i, j, !random);
                }
            }
        }

        public void printBoard()
        {
            foreach (Shape[] i in this.shapeBoard)
            {
                foreach (Shape j in i)
                {
                    Console.WriteLine(j + "\t");
                }
                Console.WriteLine("     ");
            }
            Console.WriteLine();
            for (int i = 0; i < this.ROWS; i++)
            {
                for (int j = 0; j < this.COLUMNS; j++)
                {
                    Console.WriteLine(this.board[i][j] + "\t");
                    this.board[i][j] = int.MinValue;
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n\n");
        }

        public void resetBoard()
        {
            for (int i = 0; i < this.shapeBoard.Length; i++)
            {
                for (int j = 0; j < this.shapeBoard[0].Length; j++)
                {
                    Shape shape = this.shapeBoard[i][j];
                    shape.visited = false;
                    shape.permutations = new List<int[]>();
                }
            }
            for (int i = 0; i < this.ROWS; i++)
            {
                for (int j = 0; j < this.COLUMNS; j++)
                {
                    this.board[i][j] = int.MinValue;
                }
            }
        }

        public int getColumns()
        {
            return this.COLUMNS;
        }

        public int getRows()
        {
            return this.ROWS;
        }

        public Shape[][] getShapeBoard()
        {
            return this.shapeBoard;
        }

        public int[][] getBoard()
        {
            return this.board;
        }

        public void setBoard(int[][] matrix)
        {
            this.board = matrix;
        }

    }
}

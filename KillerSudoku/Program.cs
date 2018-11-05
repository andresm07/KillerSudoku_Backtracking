using KillerSudoku.GameBoard.Logic.Board;
using KillerSudoku.GameSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KillerSudoku
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            KillerSudokuBoard board = new KillerSudokuBoard(19,19,true);
            //Solver solver = new Solver(board,10,true);
            //solver.solve();
            //board.printBoard();
        }
    }
}

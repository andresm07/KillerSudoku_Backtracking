using KillerSudoku.GameBoard.Logic.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerSudoku.GameBoard.Logic.Shapes
{
    public class SType : Shape
    {
        public SType(int range, Boolean isRandom) : base()
        {
            this.range = range;
            this.ID = ShapeTypeID.STYPE;
            this.number = new int[ID.getLength()];
            this.coordinates = new int[ID.getLength()][];
            this.orientations = ID.getOrientations();
            for (int i = 0; i < this.coordinates.Length; i++)
            {
                this.coordinates[i] = new int[2];
            }
            this.operation = ID.getOperation();
            if (isRandom)
            {
                this.objective = Generator.generateResult(this.ID, this.operation, this.range);
            }
        }

        public override string toString()
        {
            return "S";
        }
    }
}

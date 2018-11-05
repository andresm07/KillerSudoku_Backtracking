using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerSudoku.GameBoard.Logic.Shapes
{
    public class ShapeFactory
    {
        public static Shape getInstance(int range, Boolean isRandom)
        {
            Random rand = new Random();
            int option = rand.Next(Shape.getCount());
            switch(option)
            {
                case 0:
                    return new SType(range, isRandom);
                case 1:
                    return new ZType(range, isRandom);
                case 2:
                    return new LType(range, isRandom);
                case 3:
                    return new OType(range, isRandom);
                case 4:
                    return new JType(range, isRandom);
                case 5:
                    return new IType(range, isRandom);
                case 6:
                    return new TType(range, isRandom);
                default:
                    return new OneType(range, isRandom);
            }
        }
    }
}

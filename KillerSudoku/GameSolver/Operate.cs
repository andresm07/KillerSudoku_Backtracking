using KillerSudoku.GameBoard.Logic.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerSudoku.GameSolver
{
    public class Operate
    {
        private List<int> numArray;

        public Operate(List<int> numArray)
        {
            this.numArray = numArray;
        }

        public List<int[]> permutateOne(int target)
        {
            List<int[]> arrayList = new List<int[]>();
            int[] result = { (int)System.Math.Pow(target, 0.33333) };
            arrayList.Add(result);
            return arrayList;
        }

        public List<int[]> permut(int size, int target, String operation, Shape shape)
        {
            List<int[]> nums = new List<int[]>();
            int[] num = new int[size];
            rep(nums, this.numArray, num, 0, target, operation);
            if(size == 4)
            {
                shapePodeFour(shape, nums);
            }
            return nums;
        }

        private void rep(List<int[]> reps, List<int> input, int[] item, int count, int target, String operation)
        {
            if (count < item.Length)
            {
                for(int i = 0; i < input.Count; i++)
                {
                    item[count] = input[i];
                    rep(reps, input, item, count + 1, target, operation);
                }
            }
            else
            {
                switch(item.Length)
                {
                    case 2:
                        if(operateTwo(target,operation,item))
                        {
                            reps.Add((int[])item.Clone());
                        }
                        break;
                    case 4:
                        if(operateFour(target,operation,item))
                        {
                            reps.Add((int[])item.Clone());
                        }
                        break;
                }
            }
        }

        private Boolean operateTwo(int target, String operation, int[] array)
        {
            if(array[0] == array[1])
            {
                return false;
            }
            switch(operation)
            {
                case "+":
                    if((array[0] + array[1]) == target)
                    {
                        return true;
                    }
                    break;
                case "-":
                    if ((array[0] - array[1]) == target)
                    {
                        return true;
                    }
                    break;
                case "*":
                    if ((array[0] * array[1]) == target)
                    {
                        return true;
                    }
                    break;
                case "/":
                    if ((array[0] / array[1]) == target)
                    {
                        return true;
                    }
                    break;
                case "%":
                    if ((array[0] % array[1]) == target)
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }

        private Boolean operateFour(int target, String operation, int[] array)
        {
            switch(operation)
            {
                case "+":
                    if((array[0] + array[1] + array[2] + array[3]) == target)
                    {
                        return true;
                    }
                    break;
                case "-":
                    if ((array[0] - array[1] - array[2] - array[3]) == target)
                    {
                        return true;
                    }
                    break;
                case "*":
                    if ((array[0] * array[1] * array[2] * array[3]) == target)
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }

        public List<int[]> shapePodeFour(Shape shape, List<int[]> shapes)
        {
            List<int[]> toRemove = new List<int[]>();
            foreach(int[] array in shapes)
            {
                if((shape.getID() != Shape.ShapeTypeID.TTYPE) && (shape.getID() != Shape.ShapeTypeID.ZTYPE) && (shape.getID() != Shape.ShapeTypeID.LTYPE))
                {
                    if(array[0] == array[1])
                    {
                        toRemove.Add(array);
                    }
                    if(array[2] == array[3])
                    {
                        toRemove.Add(array);
                    }
                }
                if((shape.getID() != Shape.ShapeTypeID.JTYPE) && (shape.getID() != Shape.ShapeTypeID.OTYPE) && (shape.getID() != Shape.ShapeTypeID.STYPE))
                {
                    if(array[2] == array[1])
                    {
                        toRemove.Add(array);
                    }
                }
                if((shape.getID() == Shape.ShapeTypeID.ITYPE) || (shape.getID() == Shape.ShapeTypeID.OTYPE))
                {
                    if(array[2] == array[0] || array[1] == array[3])
                    {
                        toRemove.Add(array);
                    }
                }
            }
            //shapes.RemoveAll(toRemove);
            for(int i = 0; i < toRemove.Count(); i++)
            {
                if(shapes.Contains(toRemove[i]))
                {
                    shapes.Remove(toRemove[i]);
                }
            }
            return shapes;
        }
    }
}

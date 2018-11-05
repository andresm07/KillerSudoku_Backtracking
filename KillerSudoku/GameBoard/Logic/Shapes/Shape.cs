using KillerSudoku.GameBoard.Logic.Board;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KillerSudoku.GameBoard.Logic.Board.Operation;

namespace KillerSudoku.GameBoard.Logic.Shapes
{
    public abstract class Shape
    {
        public int[][][] orientations;
        public int[] number;
        public int[][] coordinates;
        public Boolean solved = false;
        private int[] head = new int[2];
        protected Operation operation;
        protected int objective;
        protected ShapeTypeID ID;
        public int range;
        protected Boolean isRandom;
        public List<int[]> permutations;
        public Boolean visited = false;

        public Boolean fits(Shape[][] board, int i, int j, int[][] matrix)
        {
            for(int k = 0; k < this.orientations.Length; k++)
            {
                if(fits(board,i,j,k,matrix))
                {
                    return true;
                }
            }
            return false;
        }

        public static int getCount()
        {
            return 9;
        }

        public void placeShape(Shape[][] shapeBoard, int[][] matrix, int i, int j, Boolean solvable)
        {
            Random rand = new Random();
            int orientation = rand.Next(this.orientations.Length);
            while(!fits(shapeBoard,i,j,orientation,matrix))
            {
                orientation = rand.Next(this.orientations.Length);
            }
            fill(shapeBoard, i, j, orientation, matrix, solvable);
        }

        public Color getColor(int[][] matrix)
        {
            return matrix[this.coordinates[0][0]][this.coordinates[0][1]] != int.MinValue ? ID.getColor() : Color.White;
        }

        private void fill(Shape[][] shapeBoard, int i, int j, int orientation, int[][] matrix, Boolean solvable)
        {
            int firstItem = -1;
            int[] solution = new int[ID.getLength()];
            this.head[0] = i;
            this.head[1] = j;
            for(int k = 0; k < this.orientations[orientation][0].Length; k++)
            {
                if(this.orientations[orientation][0][k] == 1)
                {
                    firstItem = k;
                    break;
                }
            }
            int cont = 0;
            for(int n = 0; n < this.orientations[orientation].Length; n++)
            {
                for(int m = 0; m < this.orientations[orientation][0].Length; m++)
                {
                    if(this.orientations[orientation][n][m] == 1)
                    {
                        solution[cont] = matrix[i + n][j + m - firstItem];
                        this.coordinates[cont++] = new int[] { i + n, j + m - firstItem };
                        shapeBoard[i + n][j + m - firstItem] = this;
                    }
                }
            }
            if(solvable)
            {
                if(this.getOperation().Equals(Operation.MUL))
                {
                    foreach(int num in solution)
                    {
                        if(num == 0)
                        {
                            while(this.operation.Equals(Operation.MUL))
                            {
                                this.operation = ID.getOperation();
                            }
                        }
                    }
                }
                setObjective(solution);
            }
        }

        private Boolean fits(Shape[][] shapeboard, int i, int j, int orientation, int[][] matrix)
        {
            if(i == j && i == 0 && this.orientations[orientation][0][0] == 0)
            {
                return false;
            }
            int[] solution = new int[ID.getLength()];
            int firstItem = -1;
            for(int k = 0; k < this.orientations[orientation][0].Length; k++)
            {
                if(this.orientations[orientation][0][k] == 1)
                {
                    firstItem = k;
                    break;
                }
            }
            int cont = 0;
            for(int n = 0; n < this.orientations[orientation].Length; n++)
            {
                for(int m = 0; m < this.orientations[orientation][0].Length; m++)
                {
                    if(this.orientations[orientation][n][m] == 1)
                    {
                        try
                        {
                            if(shapeboard[i+n][j+m-firstItem] != null)
                            {
                                return false;
                            }
                            else
                            {
                                solution[cont++] = matrix[i + n][j + m - firstItem];
                            }
                        }
                        catch(IndexOutOfRangeException e)
                        {
                            return false;
                        }
                    }
                }
            }
            if(getID() == ShapeTypeID.TWOTYPE)
            {
                foreach(int num in solution)
                {
                    if(num == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void setObjective(int[] numbers)
        {
            this.objective = Generator.operate(this.operation, numbers);
        }

        public Boolean isHead(int i, int j)
        {
            return head[0] == i && head[1] == j;
        }

        public int getObjective()
        {
            return this.objective;
        }

        public Operation getOperation()
        {
            return this.operation;
        }

        public ShapeTypeID getID()
        {
            return ID;
        }

        public abstract String toString();

        public int[][] setPermutation(int[][] matrix)
        {
            for(int i = 0; i < this.number.Length; i++)
            {
                matrix[this.coordinates[i][0]][this.coordinates[i][1]] = this.number[i];
            }
            return matrix;
        }

        public Boolean isValid(int[] array)
        {
            if(true)
            {
                return false;
            }
            return true;
        }

        public sealed class ShapeTypeID
        {
            public static readonly ShapeTypeID LTYPE = new ShapeTypeID("LTYPE", InnerEnum.LTYPE, new int[][][]
            {
                new int[][]
                {
                    new int[] {1, 0, 0},
                    new int[] {1, 0, 0},
                    new int[] {1, 1, 0}
                },
                new int[][]
                {
                    new int[] {1, 1, 1},
                    new int[] {1, 0, 0},
                    new int[] {0, 0, 0}
                },
                new int[][]
                {
                    new int[] {1, 1, 0},
                    new int[] {0, 1, 0},
                    new int[] {0, 1, 0}
                },
                new int[][]
                {
                    new int[] {0, 0, 1},
                    new int[] {1, 1, 1},
                    new int[] {0, 0, 0}
                }
            }, 4, Color.Pink, new int[][]
            {
            new int[] {0, 3},
            new int[] {0, 2}
            }, new Operation[] { Operation.ADD, Operation.SUB, Operation.MUL });

            public static readonly ShapeTypeID JTYPE = new ShapeTypeID("JTYPE", InnerEnum.JTYPE, new int[][][]
            {
                new int[][]
                {
                    new int[] {0, 1, 0},
                    new int[] {0, 1, 0},
                    new int[] {1, 1, 0}
                },
                new int[][]
                {
                    new int[] {1, 0, 0},
                    new int[] {1, 1, 1},
                    new int[] {0, 0, 0}
                },
                new int[][]
                {
                    new int[] {1, 1, 0},
                    new int[] {1, 0, 0},
                    new int[] {1, 0, 0}
                },
                new int[][]
                {
                    new int[] {1, 1, 1},
                    new int[] {0, 0, 1},
                    new int[] {0, 0, 0}
                }
            }, 4, Color.Aquamarine, new int[][]
            {
                new int[] {0, 3},
                new int[] {1, 3}
            }, new Operation[] { Operation.ADD, Operation.SUB, Operation.MUL });

            public static readonly ShapeTypeID OTYPE = new ShapeTypeID("OTYPE", InnerEnum.OTYPE, new int[][][]
            {
                new int[][]
                {
                    new int[] {1, 1},
                    new int[] {1, 1}
                }
            }, 4, Color.DarkCyan, new int[][]
            {
                new int[] {0, 3},
                new int[] {1, 2}
            }, new Operation[] { Operation.ADD, Operation.SUB, Operation.MUL });

            public static readonly ShapeTypeID ITYPE = new ShapeTypeID("ITYPE", InnerEnum.ITYPE, new int[][][]
            {
                new int[][]
                {
                    new int[] {1, 0, 0, 0},
                    new int[] {1, 0, 0, 0},
                    new int[] {1, 0, 0, 0},
                    new int[] {1, 0, 0, 0}
                },
                new int[][]
                {
                    new int[] {1, 1, 1, 1},
                    new int[] {0, 0, 0, 0},
                    new int[] {0, 0, 0, 0},
                    new int[] {0, 0, 0, 0}
                }
            }, 4, Color.Green, new int[0][], new Operation[] { Operation.ADD, Operation.SUB, Operation.MUL });

            public static readonly ShapeTypeID TTYPE = new ShapeTypeID("TTYPE", InnerEnum.TTYPE, new int[][][]
            {
                new int[][]
                {
                    new int[] {1, 1, 1},
                    new int[] {0, 1, 0},
                    new int[] {0, 0, 0}
                },
                new int[][]
                {
                    new int[] {0, 1, 0},
                    new int[] {1, 1, 0},
                    new int[] {0, 1, 0}
                },
                new int[][]
                {
                    new int[] {0, 1, 0},
                    new int[] {1, 1, 1},
                    new int[] {0, 0, 0}
                },
                new int[][]
                {
                    new int[] {0, 1, 0},
                    new int[] {0, 1, 1},
                    new int[] {0, 1, 0}
                }
            }, 4, Color.Yellow, new int[][] { }, new Operation[] { Operation.ADD, Operation.MUL });

            public static readonly ShapeTypeID STYPE = new ShapeTypeID("STYPE", InnerEnum.STYPE, new int[][][]
            {
                new int[][]
                {
                    new int[] {0, 1, 1},
                    new int[] {1, 1, 0},
                    new int[] {0, 0, 0}
                },
                new int[][]
                {
                    new int[] {1, 0, 0},
                    new int[] {1, 1, 0},
                    new int[] {0, 1, 0}
                }
            }, 4, Color.Purple, new int[][]
            {
                new int[] {0, 2},
                new int[] {0, 3},
                new int[] {1, 3}
            }, new Operation[] { Operation.ADD, Operation.SUB, Operation.MUL });

            public static readonly ShapeTypeID ZTYPE = new ShapeTypeID("ZTYPE", InnerEnum.ZTYPE, new int[][][]
            {
                new int[][]
                {
                    new int[] {1, 1, 0},
                    new int[] {0, 1, 1},
                    new int[] {0, 0, 0}
                },
                new int[][]
                {
                    new int[] {0, 1, 0},
                    new int[] {1, 1, 0},
                    new int[] {1, 0, 0}
                }
            }, 4, Color.DarkViolet, new int[][]
            {
                new int[] {0, 2},
                new int[] {0, 3},
                new int[] {1, 3}
            }, new Operation[] { Operation.ADD, Operation.SUB, Operation.MUL });

            public static readonly ShapeTypeID ONETYPE = new ShapeTypeID("ONETYPE", InnerEnum.ONETYPE, new int[][][]
            {
                new int[][]
                {
                    new int[] {1}
                }
            }, 1, Color.DarkGoldenrod, new int[0][], new Operation[] { Operation.EXP });

            public static readonly ShapeTypeID TWOTYPE = new ShapeTypeID("TWOTYPE", InnerEnum.TWOTYPE, new int[][][]
            {
                new int[][]
                {
                    new int[] {1, 0},
                    new int[] {1, 0}
                },
                new int[][]
                {
                    new int[] {1, 1},
                    new int[] {0, 0}
                }
            }, 2, Color.Cornsilk, new int[0][], new Operation[] { Operation.DIV, Operation.ADD, Operation.MUL, Operation.MOD, Operation.SUB });

            private static readonly IList<ShapeTypeID> valueList = new List<ShapeTypeID>();

            static ShapeTypeID()
            {
                valueList.Add(LTYPE);
            }

            public enum InnerEnum
            {
                LTYPE,
                JTYPE,
                OTYPE,
                ITYPE,
                TTYPE,
                STYPE,
                ZTYPE,
                TWOTYPE,
                ONETYPE
            }

            public readonly InnerEnum innerEnumValue;
            private readonly string nameValue;
            private readonly int ordinalValue;
            private static int nextOrdinal = 0;

            private readonly int[][][] orientations;
            private readonly int length;
            private readonly Color color;
            private readonly int[][] valid_combination;
            private readonly Operation[] op;

            internal ShapeTypeID(string name, InnerEnum innerEnum, int[][][] orientations, int length, Color color, int[][] valid_combination, Operation[] operations)
            {
                this.orientations = orientations;
                this.length = length;
                this.color = color;
                this.valid_combination = valid_combination;
                this.op = operations;

                nameValue = name;
                ordinalValue = nextOrdinal++;
                innerEnumValue = innerEnum;
            }

            public int getLength()
            {
                return this.length;
            }

            public int[][][] getOrientations()
            {
                return this.orientations;
            }

            public int[][] getValidCombination()
            {
                return this.valid_combination;
            }

            public Operation getOperation()
            {
                Random rand = new Random();
                return this.op[rand.Next(op.Length)];
            }

            public Color getColor()
            {
                return this.color;
            }

            public static IList<ShapeTypeID> values()
            {
                return valueList;
            }

            public int ordinal()
            {
                return ordinalValue;
            }

            public override string ToString()
            {
                return nameValue;
            }

            public static ShapeTypeID valueOf(string name)
            {
                foreach (ShapeTypeID enumInstance in ShapeTypeID.valueList)
                {
                    if (enumInstance.nameValue == name)
                    {
                        return enumInstance;
                    }
                }
                throw new System.ArgumentException(name);
            }
        }

    }
}

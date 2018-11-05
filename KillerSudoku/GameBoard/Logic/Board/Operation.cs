using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerSudoku.GameBoard.Logic.Board
{
    public sealed class Operation
    {
        public static readonly Operation ADD = new Operation("ADD", InnerEnum.ADD, "+");
        public static readonly Operation SUB = new Operation("SUB", InnerEnum.SUB, "-");
        public static readonly Operation DIV = new Operation("DIV", InnerEnum.DIV, "/");
        public static readonly Operation MOD = new Operation("MOD", InnerEnum.MOD, "%");
        public static readonly Operation EXP = new Operation("EXP", InnerEnum.EXP, "^");
        public static readonly Operation MUL = new Operation("MUL", InnerEnum.MUL, "*");

        private static readonly IList<Operation> valueList = new List<Operation>();

        static Operation()
        {
            valueList.Add(ADD);
            valueList.Add(SUB);
            valueList.Add(DIV);
            valueList.Add(MOD);
            valueList.Add(EXP);
            valueList.Add(MUL);
        }

        public enum InnerEnum
        {
            ADD,
            SUB,
            DIV,
            MOD,
            EXP,
            MUL
        }

        public readonly InnerEnum innerEnumValue;
        private readonly string nameValue;
        private readonly int ordinalValue;
        private static int nextOrdinal = 0;

        private readonly string symbol;


        internal Operation(string name, InnerEnum innerEnum, string symbol)
        {
            this.symbol = symbol;

            nameValue = name;
            ordinalValue = nextOrdinal++;
            innerEnumValue = innerEnum;
        }


        public string getSymbol()
        {
            return this.symbol;
        }

        public static IList<Operation> values()
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

        public static Operation valueOf(string name)
        {
            foreach (Operation enumInstance in Operation.valueList)
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

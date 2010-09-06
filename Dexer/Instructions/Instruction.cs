﻿/* Dexer Copyright (c) 2010 Sebastien LEBRETON

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. */

using Dexer.Core;
using System.Collections.Generic;
using System.Text;

namespace Dexer.Instructions
{
    public class Instruction
    {
        public OpCodes OpCode { get; set; }
        public int Offset { get; set; }
        public IList<Register> Registers { get; set; }
        public object Operand { get; set; }

        public Instruction()
        {
            Registers = new List<Register>();
        }

        public Instruction(OpCodes opcode, params Register[] registers)
            : this(opcode, null, registers)
        {
        }

        public Instruction(OpCodes opcode, object operand)
            : this(opcode, operand, null)
        {
        }

        public Instruction(OpCodes opcode, object operand, params Register[] registers) : this()
        {
            OpCode = opcode;
            Operand = operand;
            Registers = new List<Register>(registers);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(OpCode.ToString());
            for (int i = 0; i < Registers.Count; i++)
            {
                builder.Append(" ");
                builder.Append(Registers[i]);
            }
            builder.Append(" ");
            if (Operand is Instruction)
                builder.Append(string.Concat("=> {", (Operand as Instruction).Offset,"}"));
            else
                if (Operand is string)
                    builder.Append(string.Concat("\"",Operand,"\""));
                else
                    builder.Append(Operand);

            return builder.ToString();
        }

    }
}
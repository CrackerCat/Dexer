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

using System.Collections.Generic;

namespace Dexer.Instructions
{
	public class MethodBody
	{
        public DebugInfo DebugInfo { get; set; }
        public List<Register> Registers { get; set; }
        public List<Instruction> Instructions { get; set; }
        public List<ExceptionHandler> Exceptions { get; set; }
        public ushort IncomingArguments { get; set; }
        public ushort OutgoingArguments { get; set; }

        public MethodBody(int registersSize)
        {
           Registers = new List<Register>();
           for (int i = 0; i < registersSize; i++)
           {
               Registers.Add(new Register(i));
           }
           Instructions = new List<Instruction>();
           Exceptions = new List<ExceptionHandler>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Shattuck.Runtime
{
    class Environment
    {
        public List<IObject> registers;
        public IObject context;
        public Method method;
        public uint instructionPointer;
        public Environment prepared;

        public Environment(IObject context, Method method, IObject[] arguments)
        {
            this.context = context;
            this.method = method;
            registers = new List<IObject>(arguments);
            instructionPointer = 0;
            prepared = null;
        }
    }

    public class Runner
    {
        readonly Stack<Environment> stack;

        public Runner(IObject entryContext, Method entryMethod, IObject[] entryArguments) :
            this(new Environment(entryContext, entryMethod, entryArguments))
        { }

        Runner(Environment env)
        {
            stack = new Stack<Environment>();
            stack.Push(env);
        }

        Environment Current { get { return stack.Peek(); } }

        public class ExecutionEnd : Exception { }
        public class IllegalInstruction : Exception { }

        public void StepIn()
        {
            Debug.Assert(Current.instructionPointer <= Current.method.Instructions.Length);
            if (Current.instructionPointer == Current.method.Instructions.Length)
            {
                if (stack.Count == 1)
                {
                    throw new ExecutionEnd();
                }
                stack.Pop();
                return;
            }
            switch (Current.method.Instructions[Current.instructionPointer++])
            {
                case Instruction.PushAttribute pa:
                    {
                        IObject @object = Current.registers[(int)pa.Index];
                        IObject attrib = @object.Storage[(int)@object.Layout.StateMap[pa.Key]];
                        Current.registers.Add(attrib);
                        return;

                    }
                case Instruction.SetAttribute sa:
                    {
                        IObject @object = Current.registers[(int)sa.Index];
                        IObject attrib = Current.registers[(int)sa.ValueIndex];
                        @object.Storage[(int)@object.Layout.StateMap[sa.Key]] = attrib;
                        return;
                    }
                case Instruction.JumpOnNegative jmp:
                    {
                        if (Current.registers[(int)jmp.Index] is Object.Bool @bool)
                        {
                            if (@bool == Object.Bool.False)
                            {
                                Current.instructionPointer = 
                                    (uint)((int)--Current.instructionPointer + jmp.Offset);
                            }
                            return;
                        }
                        else
                        {
                            throw new IllegalInstruction();
                        }
                    }
                default:
                    throw new IllegalInstruction();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Shattuck.Runtime
{
    class Environment
    {
        public readonly List<IObject> Registers;
        public IObject Context;
        public readonly Method Method;
        public uint InstructionPointer;
        private Environment _prepared;

        public Environment(IObject context, Method method, IObject[] arguments)
        {
            this.Context = context;
            this.Method = method;
            Registers = new List<IObject>(arguments);
            InstructionPointer = 0;
            _prepared = null;
        }
    }

    public class Runner
    {
        readonly Stack<Environment> _stack;

        public Runner(IObject entryContext, TraitLayout trait, string name, IObject[] entryArguments) :
            this(new Environment(entryContext, entryContext.Layout.Dispatch(trait, name), entryArguments))
        {
        }


        Runner(Environment env)
        {
            _stack = new Stack<Environment>();
            _stack.Push(env);
        }

        private Environment Current => _stack.Peek();

        public class ExecutionEnd : Exception
        {
        }

        public class IllegalInstruction : Exception
        {
        }

        public void StepIn()
        {
            Debug.Assert(Current.InstructionPointer <= Current.Method.Instructions.Length);
            if (Current.InstructionPointer == Current.Method.Instructions.Length)
            {
                if (_stack.Count == 1)
                {
                    throw new ExecutionEnd();
                }

                _stack.Pop();
                return;
            }

            switch (Current.Method.Instructions[Current.InstructionPointer++])
            {
                case Instruction.PushAttribute pa:
                {
                    var @object = Current.Registers[(int) pa.Index];
                    var attrib = @object.Storage[(int) @object.Layout.StateMap[pa.Key]];
                    Current.Registers.Add(attrib);
                    return;
                }
                case Instruction.SetAttribute sa:
                {
                    var @object = Current.Registers[(int) sa.Index];
                    var attrib = Current.Registers[(int) sa.ValueIndex];
                    @object.Storage[(int) @object.Layout.StateMap[sa.Key]] = attrib;
                    return;
                }
                case Instruction.JumpOnNegative jmp:
                {
                    if (Current.Registers[(int) jmp.Index] is Object.Bool @bool)
                    {
                        if (@bool == Object.Bool.False)
                        {
                            Current.InstructionPointer =
                                (uint) ((int) --Current.InstructionPointer + jmp.Offset);
                        }

                        return;
                    }
                    else
                    {
                        throw new IllegalInstruction();
                    }
                }
                case Instruction.ExecuteNative en:
                {
                    en.Action(this);
                    return;
                }
                default:
                    throw new IllegalInstruction();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Shattuck.Runtime
{
    internal class Environment
    {
        public readonly List<IObject> Registers;
        public readonly Method Method;
        public uint InstructionPointer;
        public Environment Prepared;

        public Environment(IObject context, Method method, IObject[] arguments)
        {
            this.Method = method;
            Registers = new List<IObject> {context};
            Registers.AddRange(arguments);
            InstructionPointer = 0;
            Prepared = null;
        }
    }

    public class Runner
    {
        private readonly Stack<Environment> _stack;

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

        public void StepIn()
        {
            Debug.Assert(_stack.Count != 0);
            Debug.Assert(Current.InstructionPointer < Current.Method.Instructions.Length);
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
                    if (!(Current.Registers[(int) jmp.Index] is Object.Bool @bool))
                        Debug.Assert(false);
                    else
                    {
                        if (@bool == Object.Bool.False)
                        {
                            Current.InstructionPointer =
                                (uint) ((int) --Current.InstructionPointer + jmp.Offset);
                        }
                    }

                    return;
                }
                case Instruction.ExecuteNative en:
                {
                    en.Action(this);
                    return;
                }
                case Instruction.TruncateEnvironment te:
                {
                    Debug.Assert(te.Count < Current.Registers.Count); // it's illegal to truncate context at index 0
                    Current.Registers.RemoveRange(Current.Registers.Count - (int) te.Count, (int) te.Count);
                    return;
                }
                case Instruction.PrepareEnvironment pe:
                {
                    Debug.Assert(Current.Prepared is null);
                    var context = Current.Registers[(int) pe.ContextIndex];
                    var method = context.Layout.Dispatch(pe.Trait, pe.Name);
                    var arguments = (
                        from argIndex in pe.ArgumentIndices
                        select Current.Registers[(int) argIndex]
                    ).ToArray();
                    Current.Prepared = new Environment(context, method, arguments);
                    return;
                }
                case Instruction.ExhaustEnvironment _:
                {
                    Debug.Assert(!(Current.Prepared is null));
                    var env = Current.Prepared;
                    Current.Prepared = null;
                    _stack.Push(env);
                    return;
                }
                case Instruction.EscapeEnvironment ee:
                {
                    var returned = Current.Registers[(int) ee.Index];
                    _stack.Pop();
                    if (_stack.Count != 0)
                    {
                        Current.Registers.Add(returned);
                    }
                    else
                    {
                        throw new ExecutionEnd();
                    }

                    return;
                }
                default:
                    Debug.Assert(false);
                    return;
            }
        }

        public static uint Context()
        {
            return 0;
        }

        public static uint Argument(uint index)
        {
            return index + 1;
        }

        public IObject GetRegister(uint index)
        {
            return Current.Registers[(int) index];
        }
    }
}
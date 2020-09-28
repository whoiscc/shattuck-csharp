using System;

namespace Shattuck.Runtime
{
    public abstract class Instruction
    {
        public class PushAttribute : Instruction
        {
            public uint Index { get; }
            public string Key { get; }

            public PushAttribute(uint index, string key)
            {
                Index = index;
                Key = key;
            }
        }

        public class JumpOnNegative : Instruction
        {
            public uint Index { get; }
            public int Offset { get; }

            public JumpOnNegative(uint index, int offset)
            {
                Index = index;
                Offset = offset;
            }
        }

        public class SetAttribute : Instruction
        {
            public uint Index { get; }
            public string Key { get; }
            public uint ValueIndex { get; }

            public SetAttribute(uint index, string key, uint valueIndex)
            {
                Index = index;
                Key = key;
                ValueIndex = valueIndex;
            }
        }

        public class PrepareEnvironment : Instruction
        {
            public uint ContextIndex { get; }
            public TraitLayout Trait { get; }
            public string Name { get; }
            public uint[] ArgumentIndices { get; }

            public PrepareEnvironment(uint contextIndex, TraitLayout trait, string name, uint[] argumentIndices)
            {
                ContextIndex = contextIndex;
                ArgumentIndices = argumentIndices;
                Trait = trait;
                Name = name;
            }
        }

        public class ExhaustEnvironment : Instruction
        {
        }

        public class DetachEnvironment : Instruction
        {
        }

        public class EscapeEnvironment : Instruction
        {
            public uint Index { get; }

            public EscapeEnvironment(uint index)
            {
                Index = index;
            }
        }

        public class TruncateEnvironment : Instruction
        {
            public uint Count { get; }

            public TruncateEnvironment(uint count)
            {
                Count = count;
            }
        }

        public class ExecuteNative : Instruction
        {
            public ExecuteNative(Action<Runner> action)
            {
                Action = action;
            }

            public Action<Runner> Action { get; }
        }
    }
}
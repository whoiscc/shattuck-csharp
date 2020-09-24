namespace Shattuck.Runtime
{
    public interface Instruction
    {
        public readonly struct PushAttribute : Instruction
        {
            public uint Index { get; }
            public string Key { get; }
            public PushAttribute(uint index, string key)
            {
                Index = index;
                Key = key;
            }
        }

        public readonly struct JumpOnNegative : Instruction
        {
            public uint Index { get; }
            public int Offset { get; }
            public JumpOnNegative(uint index, int offset)
            {
                Index = index;
                Offset = offset;
            }
        }

        public readonly struct SetAttribute : Instruction
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

        public readonly struct PrepareEnvironment : Instruction
        {
            public uint ContextIndex { get; }
            public uint[] ArgumentIndices { get; }
            public PrepareEnvironment(uint contextIndex, uint[] argumentIndices)
            {
                ContextIndex = contextIndex;
                ArgumentIndices = argumentIndices;
            }
        }

        public readonly struct ExhaustEnvironment : Instruction { }

        public readonly struct DetachEnvironment : Instruction { }

        public readonly struct EscapeEnvironment : Instruction
        {
            public uint Index { get; }
            public EscapeEnvironment(uint index)
            {
                Index = index;
            }
        }

        public readonly struct TruncateEnvironment : Instruction
        {
            public uint Count { get; }
            public TruncateEnvironment(uint count)
            {
                Count = count;
            }
        }
    }
}

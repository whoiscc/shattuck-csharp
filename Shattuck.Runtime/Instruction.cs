namespace Shattuck.Runtime
{
    public interface Instruction
    {
        public struct PushAttribute : Instruction
        {
            uint Index { get; }
            string Key { get; }
            PushAttribute(uint index, string key)
            {
                Index = index;
                Key = key;
            }
        }

        public struct JumpOnNegative : Instruction
        {
            uint Index { get; }
            int Offset { get; }
            JumpOnNegative(uint index, int offset)
            {
                Index = index;
                Offset = offset;
            }
        }

        public struct SetAttribute : Instruction
        {
            uint Index { get; }
            string Key { get; }
            uint ValueIndex { get; }
            SetAttribute(uint index, string key, uint valueIndex)
            {
                Index = index;
                Key = key;
                ValueIndex = valueIndex;
            }
        }
    }
}

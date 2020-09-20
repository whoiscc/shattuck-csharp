namespace Shattuck.Runtime.Object
{
    public class Standard : IObject
    {
        public IObject[] Storage { get; }
        public ObjectLayout Layout { get; }

        public Standard(ObjectLayout layout)
        {
            Storage = new IObject[layout.StateMap.Count];
            Layout = layout;
        }
    }
}

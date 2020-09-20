namespace Shattuck.Runtime
{
    public interface IObject
    {
        IObject[] Storage { get; }
        ObjectLayout Layout { get; }
        object Native { get => null; set { } }
    }
}

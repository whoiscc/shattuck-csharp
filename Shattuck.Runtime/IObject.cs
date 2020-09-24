using System.Collections.Generic;

namespace Shattuck.Runtime
{
    public interface IObject
    {
        IList<IObject> Storage { get; }
        ObjectLayout Layout { get; }
        object Native { get => null; set { } }
    }
}

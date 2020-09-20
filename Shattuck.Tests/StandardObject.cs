using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shattuck.Runtime;
using Shattuck.Runtime.Object;
using System.Collections.Generic;

namespace Shattuck.Tests
{
    [TestClass]
    public class StandardObject
    {
        [TestMethod]
        public void GetStateSetState()
        {
            Dictionary<string, uint> stateMap = new Dictionary<string, uint>();
            stateMap.Add("self", 0);
            ObjectLayout layout = new ObjectLayout(stateMap, new Dictionary<string, object>());
            IObject standard = new Standard(layout);
            standard.Storage[0] = standard;
            Assert.IsNull(standard.Native);
            Assert.AreSame(standard.Storage[standard.Layout.StateMap["self"]], standard);
        }
    }
}

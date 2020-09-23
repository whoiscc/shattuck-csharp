using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shattuck.Runtime;
using System.Collections.Generic;

namespace Shattuck.Tests
{
    [TestClass]
    public class StandardObject
    {
        [TestMethod]
        public void GetStateSetState()
        {
            var stateMap = new Dictionary<string, uint>
            {
                { "self", 0 }
            };
            var layout = new ObjectLayout(stateMap);
            IObject standard = new Runtime.Object.Standard(layout);
            standard.Storage[0] = standard;
            Assert.IsNull(standard.Native);
            Assert.AreSame(standard.Storage[standard.Layout.StateMap["self"]], standard);
        }
        [TestMethod]
        public void DispatchMethod()
        {
            var methodMap = new Dictionary<string, uint>
            {
                { "hello", 0 }
            };
            var helloTrait = new TraitLayout(methodMap);
            var method = new Method();
            var layout = new ObjectLayout(new Dictionary<string, uint>());
            IObject @object = new Runtime.Object.Standard(layout);
            layout.AddImplementation(helloTrait, new Dictionary<string, Method> { { "hello", method } });
            Assert.AreSame(@object.Layout.Dispatch(helloTrait, "hello"), method);
        }
    }
}

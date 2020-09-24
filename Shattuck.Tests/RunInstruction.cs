using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shattuck.Runtime;
using System.Collections.Generic;

namespace Shattuck.Tests
{
    [TestClass]
    public class RunInstruction
    {
        [TestMethod]
        public void RunEmptyMethod()
        {
            var runner = new Runner(
                new Runtime.Object.Standard(new ObjectLayout(new Dictionary<string, uint>())),
                new Method(new Instruction[0]),
                new IObject[0]
            );
            Assert.ThrowsException<Runner.ExecutionEnd>(() => runner.StepIn());
        }
    }
}

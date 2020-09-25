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
            var trait = new TraitLayout(new Dictionary<string, uint>
            {
                {"empty", 0},
            });
            var layout = new ObjectLayout(new Dictionary<string, uint>());
            layout.AddImplementation(trait, new Dictionary<string, Method>
            {
                {"empty", new Method(new Instruction[0])}
            });
            var runner = new Runner(
                new Runtime.Object.Standard(layout), trait, "empty", new IObject[0]);
            Assert.ThrowsException<Runner.ExecutionEnd>(() => runner.StepIn());
        }
    }
}
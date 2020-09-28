using Shattuck.Runtime;
using Shattuck.Runtime.Object;

namespace Shuttack.Command
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var _ = args;
            var runner = new Runner(
                new String("cowsay"), String.NativeTrait, "print", new IObject[0]);
            try
            {
                while (true)
                {
                    runner.StepIn();
                }
            }
            catch (Runner.ExecutionEnd)
            {
            }
        }
    }
}
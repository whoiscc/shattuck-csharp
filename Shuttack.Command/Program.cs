using System;
using Shattuck.Runtime;
using String = Shattuck.Runtime.Object.String;

namespace Shuttack.Command
{
    class Program
    {
        static void Main(string[] args)
        {
            Runner runner = new Runner(
                new String("cowsay"), String.NativeTrait, "print", new IObject[0]);
            runner.StepIn();
        }
    }
}
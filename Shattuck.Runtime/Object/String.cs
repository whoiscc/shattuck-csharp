using System;
using System.Collections.Generic;

namespace Shattuck.Runtime.Object
{
    public class String : IObject
    {
        public static readonly TraitLayout NativeTrait = new TraitLayout(new Dictionary<string, uint>
        {
            {"print", 0},
        });

        private static readonly ObjectLayout AssociatedLayout =
            new ObjectLayout(new Dictionary<string, uint>());

        static String()
        {
            AssociatedLayout.AddImplementation(NativeTrait, new Dictionary<string, Method>
            {
                {
                    "print", new Method(new Instruction[]
                    {
                        new Instruction.ExecuteNative(Print),
                    })
                }
            });
        }

        public ObjectLayout Layout => AssociatedLayout;
        public IList<IObject> Storage => new IObject[0];
        public string Native;

        public String(string rawString)
        {
            Native = rawString;
        }

        private static void Print(Runner runner)
        {
            Console.Out.Write("hello, shattuck!");
        }
    }
}
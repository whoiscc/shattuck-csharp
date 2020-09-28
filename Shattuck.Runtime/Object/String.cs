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
                        new Instruction.EscapeEnvironment(0), 
                    })
                }
            });
        }

        public ObjectLayout Layout => AssociatedLayout;
        public IList<IObject> Storage => new IObject[0];
        private string _native;

        public object Native
        {
            get => _native;
            set => _native = (string) value;
        }

        public String(string rawString)
        {
            _native = rawString;
        }

        private static void Print(Runner runner)
        {
            var raw = runner.GetRegister(Runner.Context()).Native as string;
            Console.Out.Write($"hello, {raw}!");
        }
    }
}
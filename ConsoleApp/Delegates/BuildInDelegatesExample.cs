using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Delegates
{
    public class BuildInDelegatesExample
    {
        //public delegate void OddNumberDelegate();
        //public event OddNumberDelegate OddNumberEvent;

        public event EventHandler<OddNumberEventArgs> OddNumberEvent;

        public void Add(int a, int b)
        {
            var result = a + b;
            Console.WriteLine(result);
            if (result % 2 != 0)
                OddNumberEvent?.Invoke(this, new OddNumberEventArgs { Value = result });
        }

        private int _counter = 0;

        //void Count(object sender, EventArgs eventArgs)
        void Count()
        {
            _counter++;
        }

        public bool Substract(int a, int b)
        {
            var result = a - b;
            Console.WriteLine(result);
            return result % 2 != 0;
        }

        public void Test()
        {
            OddNumberEvent += delegate (object sender, OddNumberEventArgs eventArgs) { Count(); };
            OddNumberEvent += delegate (object sender, OddNumberEventArgs eventArgs) { Console.WriteLine($"Odd number {eventArgs.Value}!"); };
            Method(Add, Substract);

            Console.WriteLine($"Counter: {_counter}");
        }

        //public delegate void AddDelegate(int a, int b);
        //public delegate bool SubstractDelegate(int a, int b);
        //private void Method(AddDelegate add, SubstractDelegate sub)
        private void Method(Action<int, int> add, Func<int, int, bool> sub)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int ii = 0; ii < 3; ii++)
                {
                    add(i, ii);
                    if (sub(i, ii))
                        OddNumberEvent?.Invoke(this, new OddNumberEventArgs());
                }
            }
        }


        public class OddNumberEventArgs : EventArgs
        {
            public int Value { get; set; }
        }
    }
}

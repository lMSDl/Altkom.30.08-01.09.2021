using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Delegates
{
    public class DelegateExample
    {
        public delegate void NoParametersNoReturnDelegate();
        public delegate void ParametersNoReturnDelegate(string @string);
        public delegate bool ParametersReturnDelegate(int int1, int int2);


        public void Func1()
        {
            Console.WriteLine("Func1");
        }
        public void Func2(string someString)
        {
            Console.WriteLine(someString);
        }
        public bool Func3(int x, int y)
        {
            Console.WriteLine("x=" + x + " y=" + y);
            Console.WriteLine(string.Format("x={0} y={1}", x, y));
            Console.WriteLine("x={0} y={1}", x, y);
            Console.WriteLine($"x={x} y={y}");

            return x == y;
        }

        ParametersReturnDelegate Delegate3 { get; set; }

        public void Test()
        {
            var delegate1 = new NoParametersNoReturnDelegate(Func1);
            delegate1();

            ParametersNoReturnDelegate delegate2 = null;

            if(delegate2 != null)
                delegate2("Func2");

            delegate2?.Invoke("Func2null");

            delegate2 = Func2;

            delegate2("Func2");
            delegate2?.Invoke("Func2");

            Delegate3 = Func3;

            for (int i = 0; i < 3; i++)
            {
                for (int ii = 0; ii < 3; ii++)
                {
                    if(Delegate3(i, ii))
                        Console.WriteLine("==");
                }
            }

        }
    }
}

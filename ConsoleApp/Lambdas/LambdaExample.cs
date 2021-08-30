using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Lambdas
{
    public class LambdaExample
    {
        Func<int, int, int> Calculator { get; set; }
        Action<int> SomeAction { get; set; }
        Action AnotherAction { get; set; }

        public void Test()
        {
            Calculator += delegate (int param1, int param2) { return param1 + param2; };
            Calculator += (param1, param2) => { return param1 + param2; };
            Calculator += (param1, param2) => param1 + param2;

            SomeAction += delegate (int a) { Console.WriteLine(a); };
            SomeAction += (a) => Console.WriteLine(a);
            SomeAction += a => Console.WriteLine(a);

            AnotherAction += delegate () { Console.WriteLine("Action!"); };
            AnotherAction += () => Console.WriteLine("Action!");
            AnotherAction += () => {
                Console.WriteLine("Another!");
                Console.WriteLine("Action!");
            };


            SomeMethod(x =>
            {
                var @string = x.Replace(",", "'");
                Console.WriteLine(@string);
            }, "A,B,C,D,E");
        }


        void SomeMethod(Action<string> stringAction, string stringParam)
        {
            stringAction(stringParam);
        }

    }
}

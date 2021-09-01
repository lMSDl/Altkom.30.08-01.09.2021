using ConsoleApp.Delegates;
using ConsoleApp.Lambdas;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new ServiceReference.PeopleServiceClient();
            var people = service.ReadAll();

            var item = new LinqExample();

            //item.OddNumbDelegate = null;
            //item.OddNumberEvent += null;
                
                item.Test();

            Shape2D shape2D = new Elipse("Elipsa");
            shape2D.Height = 2;
            shape2D.Width = 5;

            Console.WriteLine(shape2D.Area);

            Shape1D shape1D = shape2D;
            shape1D.ShowParams();


            Nullable<int> a = null;
            int? b = 5;
            int c;

            if(a - b == 0 || a - b == null)
            {
                c = (a + b) ?? 0;
            }
            else
            {
                var result = a - b;
                //if (result != null)
                if(result.HasValue)
                    c = result.Value;
                else
                    c = 0;
            }

            c = (a - b == 0 || a - b == null) ? ((a + b) ?? 0) : (a - b ?? 0);
            c = a - b == 0 || a - b == null ? (a + b) ?? 0 : a - b ?? 0;
            c = (a - b == 0 || a - b == null ? a + b : a - b) ?? 0;


            Console.ReadLine();
        }
    }
}

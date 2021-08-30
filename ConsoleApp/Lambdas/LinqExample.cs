using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Lambdas
{
    public class LinqExample
    {
        int[] numbers = new[] { 1, 3, 4, 2, 5, 7, 8, 6, 9, 0 };
        List<string> strings = "wlazł kotek na płotek i mruga".Split(' ').ToList();

        List<Person> students = new List<Person>
        {
            new Person { FirstName = "Adam", LastName = "Adamski", BirthDate = new DateTime(1978, 2, 21) },
            new Person { FirstName = "Ewa", LastName = "Ewowska", BirthDate = new DateTime(1990, 1, 1), Gender = Gender.Female  } ,
            new Person { FirstName = "Adam", LastName = "Ewowska", BirthDate = new DateTime(1978, 2, 21) },
            new Person { FirstName = "Ewa", LastName = "Adamska", BirthDate = new DateTime(1990, 1, 1), Gender = Gender.Female  } ,
            new Person { FirstName = "Piotr", LastName = "Adamski", BirthDate = new DateTime(1978, 2, 21) },
            new Person { FirstName = "Kamila", LastName = "Ewowska", BirthDate = new DateTime(1990, 1, 1), Gender = Gender.Female  } ,
    };

        public void Test()
        {
            var query1 = from number in numbers where number > 4 select number;
            var query2 = numbers.Where(x => x > 4).ToList();
            var query3 = numbers.Where(x => x > 4).OrderBy(x => x).ToList();

            var query4 = strings.Where(x => x.Length == 5).Select(x => x.ToUpper()).ToList();
            var query5 = strings.Where(x => x.Contains("a")).Select(x => x.Length).ToList();

            var query6 = students.Skip(4).Where(x => x.FirstName == "Adam").FirstOrDefault();
            var query7 = students.OrderByDescending(x => x.LastName).ThenBy(x => x.FirstName).ToList();
        }
    }
}

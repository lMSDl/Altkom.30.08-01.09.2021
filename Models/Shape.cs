using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public abstract class Shape
    {
        protected string Name { get; set; }

        public Shape()
        {

        }

        public Shape(string name)
        {
            Name = name;
        }

        public virtual void ShowParams()
        {
            Console.WriteLine(Name);
        }
    }
}

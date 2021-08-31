using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public abstract class Shape2D : Shape1D, IArea
    {
        protected Shape2D()
        {
        }

        protected Shape2D(string name) : base(name)
        {
        }

        public int Height { get; set; }

        public abstract double Area { get; }
        //public abstract double GetArea();

        public override void ShowParams()
        {
            base.ShowParams();
            Console.WriteLine($"{nameof(Height)}: {Height}");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public abstract class Shape1D : Shape
    {
        protected Shape1D()
        {
        }

        protected Shape1D(string name) : base(name)
        {
        }

        public int Width { get; set; }

        public override void ShowParams()
        {
            base.ShowParams();
            Console.WriteLine($"{nameof(Width)}: {Width}");
        }
    }
}

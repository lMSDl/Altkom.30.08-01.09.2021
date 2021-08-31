using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Elipse : Shape2D
    {
        public Elipse()
        {
        }

        public Elipse(string name) : base(name)
        {
        }

        public override double Area => Math.PI * Height / 2.0 * Width / 2.0;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Rectangle : Shape2D
    {
        public Rectangle()
        {
        }

        public Rectangle(string name) : base(name)
        {
        }

        public override double Area => Width * Height;

        //        public double Area => Width * Height;
        //        {
        //            get
        //            {
        //                return Width* Height;
        //            }
        //        }

        //public double GetArea()
        //{
        //    return Width * Height;
        //}
    }
}

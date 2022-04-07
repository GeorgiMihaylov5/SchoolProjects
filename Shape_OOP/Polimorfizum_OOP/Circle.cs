using System;
using System.Collections.Generic;
using System.Text;

namespace Polimorfizum_OOP
{
    public class Circle : Shape
    {
        private double r;
        public Circle(double r)
        {
            this.R = r;
        }
        public double R
        {
            get { return r; }
            set { r = value; }
        }
        public override double CalculateArea()
        {
            return Math.PI * Math.Pow(R, 2);
        }

        public override double CalculatePerimeter()
        {
            return Math.PI * 2 * R;
        }
        public sealed override string Draw()
        {
            return base.Draw() + " Circle";
        }
    }
}

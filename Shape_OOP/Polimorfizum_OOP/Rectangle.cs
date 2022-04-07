using System;
using System.Collections.Generic;
using System.Text;

namespace Polimorfizum_OOP
{
    public class Rectangle : Shape
    {
        private double length;
        private double wight;

        public Rectangle(double length,double wight)
        {
            this.Length = length;
            this.Wight = wight;
        }

        public double Length
        {
            get { return length; }
            set { length = value; } 
        }
        public double Wight
        {
            get { return wight; }
            set { wight = value; }
        }
        public override double CalculateArea()
        {
            return length * wight;
        }

        public override double CalculatePerimeter()
        {
            return  2 * length + 2 * wight;
        }
        public sealed override string Draw()
        {
            return base.Draw() + " Rectangle";
        }
    }
}

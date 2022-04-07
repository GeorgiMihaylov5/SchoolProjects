using System;
using System.Collections.Generic;

namespace Polimorfizum_OOP
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Shape> shapes = new List<Shape>();
            Shape shape1 = new Circle(5.0);
            Shape shape2 = new Rectangle(5.3, 8.0);
            Shape shape3 = new Rectangle(7.0, 8.0);

            shapes.Add(shape1);
            shapes.Add(shape2);
            shapes.Add(shape3);

            foreach (var item in shapes)
            {
                Console.WriteLine(item.Draw());
                Console.WriteLine($"Area = {item.CalculateArea():f2}");
                Console.WriteLine($"Perimeter = {item.CalculatePerimeter():f2}");
            }
        }
    }
}

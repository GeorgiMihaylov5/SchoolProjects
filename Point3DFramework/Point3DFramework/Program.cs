using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Point3DFramework
{
    class Program
    {
        private static readonly object calculate = new object();
        //private readonly object _point2;
        //private readonly object _point3;
        //static ConsoleKeyInfo key;
        static Point3D point1 = new Point3D();
        static Point3D point2 = new Point3D();
        static Thread t1;
        static Thread t2;
        static Thread t3;
        static void Main(string[] args)
        {
            ThreadStart ts1 = new ThreadStart(Point1);
            t1 = new Thread(ts1);
            t1.Start();

            ThreadStart ts2 = new ThreadStart(Point2);
            t2 = new Thread(ts2);
            t2.Start();

            ThreadStart ts3 = new ThreadStart(Print);
            t3 = new Thread(ts3);
            t3.Start();
        }
        static double Calculate(Point3D point1, Point3D point2)
        {
            double a = Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2) + Math.Pow(point2.Z - point1.Z, 2);
            return Math.Sqrt(a);
        }
        static void Point1()
        {
            ConsoleKeyInfo key1 = Console.ReadKey(true);

            do
            {
                try
                {
                    SwitchPoint1(key1);
                }
                finally
                {
                    key1 = Console.ReadKey(true);
                }
            }
            while (key1.Key != ConsoleKey.Escape);
        }
        static void Point2()
        {
            ConsoleKeyInfo key2 = Console.ReadKey(true);

            do
            {
                try
                {
                    SwitchPoint2(key2);
                }
                finally
                {
                    key2 = Console.ReadKey(true);
                }
            }
            while (key2.Key != ConsoleKey.Escape);
        }


        static void SwitchPoint1(ConsoleKeyInfo key1)
        {
            switch (key1.Key)
            {
                case ConsoleKey.UpArrow:
                    point1.X++;
                    break;
                case ConsoleKey.DownArrow:
                    point1.X--;
                    break;
                case ConsoleKey.LeftArrow:
                    point1.Y--;
                    break;
                case ConsoleKey.RightArrow:
                    point1.Y++;
                    break;
                case ConsoleKey.NumPad1:
                    point1.Z++;
                    break;
                case ConsoleKey.NumPad2:
                    point1.Z--;
                    break;
                default:
                    SwitchPoint2(key1);
                    break;
            }
        }
        static void SwitchPoint2(ConsoleKeyInfo key2)
        {
            switch (key2.Key)
            {
                case ConsoleKey.W:
                    point2.X++;
                    break;
                case ConsoleKey.S:
                    point2.X--;
                    break;
                case ConsoleKey.A:
                    point2.Y--;
                    break;
                case ConsoleKey.D:
                    point2.Y++;
                    break;
                case ConsoleKey.D1:
                    point2.Z++;
                    break;
                case ConsoleKey.D2:
                    point2.Z--;
                    break;
                default:
                    SwitchPoint1(key2);
                    break;
            }
        }
        static void Print()
        {
            while (true)
            {
                Console.Write($"\r[{point1.X}, {point1.Y}, {point1.Z}]:[{point2.X}, {point2.Y}, {point2.Z}]   {Calculate(point1, point2):f2}");
            }
        }
    }
}

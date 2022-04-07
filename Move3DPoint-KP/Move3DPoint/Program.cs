using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Move3DPoint
{
    class Program
    {
        static Thread t1;
        static Thread t2;
        static Thread t3;
        static Point3D p1;
        static Point3D p2;
        static void Main(string[] args)
        {
            double[] coordinates1 = Console.ReadLine().Split().Select(double.Parse).ToArray();
            p1 = new Point3D(coordinates1[0],
                coordinates1[1], coordinates1[2]);

            double[] coordinates2 = Console.ReadLine().Split().Select(double.Parse).ToArray();
            p2 = new Point3D(coordinates2[0],
                coordinates2[1], coordinates2[2]);

            t3 = new Thread(new ThreadStart(Calculate));
            t3.Start();
        }
        private static object obj=new object();
        public static void Calculate()
        {
            while (true)
            {
                Monitor.Enter(obj);
                double xValue = Math.Pow(p2.X - p1.X,2);
                double yValue = Math.Pow(p2.Y - p1.Y, 2);
                double zValue = Math.Pow(p2.Z - p1.Z, 2);
                double ab = Math.Sqrt(xValue+yValue+zValue);
                Console.WriteLine(ab);
                Monitor.Exit(obj);
            }
        }
    }
}

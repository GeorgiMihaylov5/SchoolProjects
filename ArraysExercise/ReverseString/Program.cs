using System;
using System.Linq;

namespace ReverseString
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] arr = Console.ReadLine().Split().Reverse().ToArray();
            Console.WriteLine(string.Join(" ", arr));
        }
    }
}

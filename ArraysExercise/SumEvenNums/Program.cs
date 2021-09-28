using System;
using System.Linq;

namespace SumEvenNums
{
    class Program
    {
        static void Main(string[] args)
        {
            var nums = Console.ReadLine().Split().Select(int.Parse).ToArray();
            Console.WriteLine(nums.Where(x => x % 2 == 0).Sum());
        }
    }
}

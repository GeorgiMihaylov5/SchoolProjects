using System;
using System.Collections.Generic;
using System.Linq;

namespace PrintEvenNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            var nums = Console.ReadLine().Split().Select(int.Parse).ToList();
            var result = new List<int>();

            foreach (var item in nums)
            {
                if (item % 2 == 0)
                {
                    result.Add(item);
                }
            }
            Console.WriteLine(string.Join(" ",result));
        }
    }
}

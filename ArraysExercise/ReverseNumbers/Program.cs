using System;
using System.Linq;
using System.Collections.Generic;


namespace ReverseNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            int[] nums = new int[n];

            for (int i = 0; i < n; i++)
            {
                int num = int.Parse(Console.ReadLine());
                nums[i] = num;
            }
            Console.WriteLine(string.Join(" ", nums.Reverse()));
        }
    }
}

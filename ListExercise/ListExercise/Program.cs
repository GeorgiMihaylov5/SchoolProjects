
using System;
using System.Linq;

namespace ListExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = Console.ReadLine().Split().Select(int.Parse).ToList();

            list = list.Where(x => x > 0).Reverse().ToList();

            if (list.Count() == 0)
            {
                Console.WriteLine("empty");
            }
            else
            {
                Console.WriteLine(string.Join(" ", list));
            }
        }
    }
}

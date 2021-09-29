using System;
using System.Collections.Generic;
using System.Linq;

namespace ListOfNames
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> list = Console.ReadLine().Split(", ").ToList();

            foreach (var item in list)
            {
                var person = item.Split().ToList();
                person.Reverse();
                Console.WriteLine(string.Join(" ", person));
            }
        }
    }
}

using System;
using System.Linq;

namespace ListManipulationBasics
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = Console.ReadLine().Split().Select(int.Parse).ToList();

            var input = Console.ReadLine();

            while (input != "end")
            {
                var commands = input.Split().ToArray();
                var cmd = commands[0];

                if (cmd == "Add")
                {
                    var num = int.Parse(commands[1]);
                    list.Add(num);
                }
                else if (cmd == "Remove")
                {
                    var num = int.Parse(commands[1]);
                    list.Remove(num);
                }
                else if (cmd == "RemoveAt")
                {
                    var index = int.Parse(commands[1]);
                    list.RemoveAt(index);
                }
                else if (cmd == "Insert")
                {
                    var num = int.Parse(commands[1]);
                    var index = int.Parse(commands[2]);

                    list.Insert(index,num);
                }

                input = Console.ReadLine();
            }
            Console.WriteLine(string.Join(" ", list));
        }
    }
}

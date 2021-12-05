using System;
using System.Linq;

namespace ExTema2
{
    class Program
    {
        static void Main(string[] args)
        {
            var nums = Console.ReadLine().Split().Select(int.Parse).ToList();

            string input = Console.ReadLine();

            while (input != "stop")
            {
                string[] commands = input.Split().ToArray();
                string cmd = commands[0];

                if (cmd == "ins")
                {
                    int index = int.Parse(commands[1]);
                    int elm = int.Parse(commands[2]);

                    nums.Insert(index, elm);
                }
                else if (cmd == "contains")
                {
                    int elm = int.Parse(commands[1]);

                    if (!nums.Contains(elm))
                    {
                        Console.WriteLine("-1");
                    }
                    else
                    {
                        Console.WriteLine(nums.IndexOf(elm));
                    }
                }
                else if (cmd == "remove")
                {
                    int index = int.Parse(commands[1]);

                    nums.RemoveAt(index);
                }
                else if (cmd == "add")
                {
                    int elm = int.Parse(commands[1]);

                    if (!nums.Contains(elm))
                    {
                        nums.Add(elm);
                    }
                }
                else if (cmd == "removeAll")
                {
                    int elm = int.Parse(commands[1]);

                    nums.RemoveAll(x => x == elm);
                }
                else if (cmd == "printGreater")
                {
                    int elm = int.Parse(commands[1]);

                    Console.WriteLine(string.Join(" ", nums.Where(x => x > elm)));
                }

                input = Console.ReadLine();
            }
            Console.WriteLine(string.Join(", ", nums));
        }
    }
}

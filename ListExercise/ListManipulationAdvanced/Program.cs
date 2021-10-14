using System;
using System.Linq;

namespace ListManipulationAdvanced
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = Console.ReadLine().Split().Select(int.Parse).ToList();

            var input = Console.ReadLine();
            var isMakeChange = false;

            while (input != "end")
            {
                var arr = input.Split().ToArray();

                var cmd = arr[0];

                if (cmd == "Add")
                {
                    var num = int.Parse(arr[1]);
                    list.Add(num);
                    isMakeChange = true;
                }
                else if (cmd == "Remove")
                {
                    var num = int.Parse(arr[1]);
                    list.Remove(num);
                    isMakeChange = true;
                }
                else if (cmd == "RemoveAt")
                {
                    var index = int.Parse(arr[1]);
                    list.RemoveAt(index);
                    isMakeChange = true;
                }
                else if (cmd == "Insert")
                {
                    var num = int.Parse(arr[1]);
                    var index = int.Parse(arr[2]);

                    list.Insert(index, num);
                    isMakeChange = true;
                }

                switch (cmd)
                {
                    case "Contains":
                        {
                            if (list.Contains(int.Parse(arr[1])))
                            {
                                Console.WriteLine("Yes");
                            }
                            else
                            {
                                Console.WriteLine("No such number");
                            }
                            break;
                        }
                    case "PrintEven":
                        {
                            Console.WriteLine(string.Join(" ", list.Where(x => x % 2 == 0)));
                            break;
                        }
                    case "PrintOdd":
                        {
                            Console.WriteLine(string.Join(" ", list.Where(x => x % 2 != 0)));
                            break;
                        }
                    case "GetSum":
                        {
                            Console.WriteLine(list.Sum());
                            break;
                        }
                    case "Filter":
                        {
                            var condition =arr[1].ToString();
                            var number = int.Parse(arr[2]);

                            switch (condition)
                            {
                                case ">":
                                    {
                                        Console.WriteLine(string.Join(" ", list.Where(x => x > number)));
                                        break;
                                    }
                                case "<":
                                    {
                                        Console.WriteLine(string.Join(" ", list.Where(x => x < number)));
                                        break;
                                    }
                                case ">=":
                                    {
                                        Console.WriteLine(string.Join(" ", list.Where(x => x >= number)));
                                        break;
                                    }
                                case "<=":
                                    {
                                        Console.WriteLine(string.Join(" ", list.Where(x => x <= number)));
                                        break;
                                    }
                            }
                            break;
                        }
                }
                input = Console.ReadLine();
            }
            if (isMakeChange)
            {
                Console.WriteLine(string.Join(" ",list));
            }
        }
    }
}

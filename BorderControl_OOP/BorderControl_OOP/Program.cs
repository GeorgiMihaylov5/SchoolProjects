using System;
using System.Linq;
using System.Collections.Generic;

namespace BorderControl_OOP
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IIdentifiable> population = new List<IIdentifiable>();
            while (true)
            {
                string[] input = Console.ReadLine().Split().ToArray();
                if(input[0]=="End")
                {
                    break;
                }
                if (input.Length == 3)
                {
                    IIdentifiable citizens = new Citizens(input[0],
                        int.Parse(input[1]), input[2]);
                    population.Add(citizens);
                }
                else if(input.Length==2)
                {
                    IIdentifiable robot = new Robot(input[0], input[1]);
                    population.Add(robot);
                }
            }
            string fakeIdentifier = Console.ReadLine();

            List<string> forbiddenPopulation = population.Where(x => x.Id.EndsWith(fakeIdentifier)).Select(x=>x.Id).ToList();

            foreach (var item in forbiddenPopulation)
            {
                Console.WriteLine(item);
            }
        }
    }
}

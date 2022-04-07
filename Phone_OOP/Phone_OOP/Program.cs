using System;
using System.Linq;

namespace Phone_OOP
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] numbers = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();
            string[] sites = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();

            Smartphone smartphone = new Smartphone();
            StationaryPhone stationaryPhone = new StationaryPhone();


            for (int i = 0; i < numbers.Length; i++)
            {
                try
                {
                    if (numbers[i].Length == 10)
                        Console.WriteLine($"Calling... {smartphone.CallNumber(numbers[i])}");
                    if (numbers[i].Length == 7)
                        Console.WriteLine($"Dialing... {stationaryPhone.CallNumber(numbers[i])}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            for (int i = 0; i < sites.Length; i++)
            {
                try
                {
                    Console.WriteLine($"Browsing: {smartphone.BrowsingSite(sites[i])}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Store2
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            var products = new List<Product>();

            for (int i = 0; i < n; i++)
            {
                //При exсeption задачата продължава, защото е в цикъл
                try
                {
                    string[] input = Console.ReadLine().Split(',').ToArray();
                    var product = new Product(input[0], int.Parse(input[1]), double.Parse(input[2]));

                    products.Add(product);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            foreach (var item in products.OrderByDescending(x => x.ProductName).ThenBy(x => x.Price))
            {
                item.Discount();
                Console.WriteLine(item.ToString());
            }
        }
    }
}

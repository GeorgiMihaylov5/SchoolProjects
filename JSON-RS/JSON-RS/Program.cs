using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JSON_RS
{
    class Program
    {
        static void Main(string[] args)
        {
            Product singleProduct = new Product(
                1, "Bob", 2.40m, 10, new DateTime(2022, 04, 24));
            string singleJSON = JsonConvert.SerializeObject(singleProduct);

            Console.WriteLine("Single product");
            Console.WriteLine(singleJSON);
            Console.WriteLine(new string('-', 20));

            List<Product> listProducts = new List<Product>()
            {
                new Product(1, "CocaCola", 2.60m, 15, new DateTime(2023, 05, 05)),
                new Product(2, "Fanta", 2.60m, 20, new DateTime(2023, 07, 16)),
                new Product(3, "Pepsi", 2.50m, 8, new DateTime(2021, 10, 01)),
                new Product(4, "Mirinda", 2.49m, 3, new DateTime(2022, 12, 25))
            };
            string listJSON = JsonConvert.SerializeObject(listProducts);
            Console.WriteLine("List of products:");
            Console.WriteLine(listJSON);
            Console.WriteLine(new string('-', 20));

            string dJSON = @"['Smal','Medium','Large']";
            JArray jArray = JArray.Parse(dJSON);
            foreach (var item in jArray)
            {
                Console.WriteLine(item);
            }
        }
    }
}

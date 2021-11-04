using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Numerics;

namespace _3._Product_Shop
{
    class Program
    {
        static void Main(string[] args)
        {
            string command = Console.ReadLine();
            SortedDictionary<string, Dictionary<string, double>> stores = new SortedDictionary<string, Dictionary<string, double>>();


            while (command != "Revision")
            {
                string[] commandArgs = command.Split(", ", StringSplitOptions.RemoveEmptyEntries);
                string storeName = commandArgs[0];
                string product = commandArgs[1];
                double productPrice = double.Parse(commandArgs[2]);

                if (stores.ContainsKey(storeName) == false)
                {
                    stores[storeName] = new Dictionary<string, double>();

                }
                stores[storeName].Add(product, productPrice);

                command = Console.ReadLine();
            }

            foreach (var item in stores)
            {
                Console.WriteLine($"{item.Key}->");

                foreach (var product in item.Value)
                {
                    Console.WriteLine($"Product: {product.Key}, Price: {product.Value}");
                }
            }
        }
    }
}

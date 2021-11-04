using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Numerics;

namespace _6._Supermarket
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<string> customers = new Queue<string>();
            string customer = Console.ReadLine();
            while (customer != "End")
            {
                if (customer == "Paid")
                {
                    foreach (var customer1 in customers)
                    {
                        Console.WriteLine(customer1);
                    }
                    customers.Clear();
                    customer = Console.ReadLine();
                    continue;
                }

                customers.Enqueue(customer);
                customer = Console.ReadLine();
            }

            Console.WriteLine($"{customers.Count} people remaining.");
        }
    }
}

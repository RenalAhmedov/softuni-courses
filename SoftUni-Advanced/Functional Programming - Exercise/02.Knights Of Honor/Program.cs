using System;
using System.Linq;
using System.Collections.Generic;
namespace _02.Knights_Of_Honor
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<List<string>> printCollection = args => args.ForEach(x => Console.WriteLine($"Sir {x}"));

            List<string> input = Console.ReadLine().Split().ToList();

            printCollection(input);

        }
    }
}

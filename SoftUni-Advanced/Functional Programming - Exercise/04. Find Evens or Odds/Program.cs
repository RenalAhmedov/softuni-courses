using System;
using System.Collections.Generic;
using System.Linq;

namespace _04._Find_Evens_or_Odds
{
    class Program
    {
        static void Main(string[] args)
        {
            Predicate<int> isEven = number => number % 2 == 0;

            Action<int[]> printIntegers = inputNumbers => Console.WriteLine(string.Join(" ", inputNumbers));

            int[] range = Console.ReadLine().Split().Select(int.Parse).ToArray();

            List<int> numbers = Enumerable.Range(range[0], range[1] - range[0] + 1).ToList();

            string evenOdd = Console.ReadLine();
            if (evenOdd == "even")
            {
                printIntegers(numbers.FindAll(isEven).ToArray());
            }
            else
            {
                printIntegers(numbers.FindAll(x => !isEven(x)).ToArray());
            }
        }
    }
}

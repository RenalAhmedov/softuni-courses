using System;
using System.Linq;

namespace _02.PrintNumbersInReverseOrder
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            string[] numbers = new string[n];


            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = Console.ReadLine();
            }

            for (int i = numbers.Length - 1; i >= 0; i--)
            {
                Console.Write($"{numbers[i]} ");
            }


        }
    }
}
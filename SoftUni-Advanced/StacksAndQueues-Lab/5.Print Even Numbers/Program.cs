using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Numerics;

namespace _5.Print_Even_Numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> input = new Queue<int>(Console.ReadLine().Split().Select(int.Parse).Reverse());

            while (!input.All(i => i % 2 == 0))
            {
                if (input.Peek() % 2 == 0)
                {
                    input.Enqueue(input.Dequeue());
                }
                else
                {
                    input.Dequeue();
                }
            }

            Console.WriteLine(string.Join(", ", input.Reverse()));
        }
    }
}

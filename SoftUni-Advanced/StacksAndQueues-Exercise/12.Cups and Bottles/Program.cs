using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Numerics;

namespace _12.Cups_and_Bottles
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack<int> cups = new Stack<int>(Console.ReadLine().Split().Select(int.Parse).Reverse());
            Stack<int> bottles = new Stack<int>(Console.ReadLine().Split().Select(int.Parse));
            int wastedWater = 0;

            while (cups.Count > 0 && bottles.Count > 0)
            {
                if (cups.Peek() - bottles.Peek() <= 0)
                {
                    wastedWater += Math.Abs(cups.Peek() - bottles.Peek());
                    cups.Pop();
                }
                else
                {
                    cups.Push(cups.Pop() - bottles.Peek());
                }
                bottles.Pop();
            }

            Console.WriteLine(cups.Count == 0 ? $"Bottles: {string.Join(" ", bottles)}" : $"Cups: {string.Join(" ", cups)}");
            Console.WriteLine($"Wasted litters of water: {wastedWater}");
        }
    }
}

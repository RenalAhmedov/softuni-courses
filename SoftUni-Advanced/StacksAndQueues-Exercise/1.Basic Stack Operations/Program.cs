using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BasicStackOperations
{
    class BasicStackOperations
    {
        static void Main(string[] args)
        {
            int[] integers = Console.ReadLine().Split().Select(int.Parse).ToArray();
            Stack<int> stack = new Stack<int>(Console.ReadLine().Split().Select(int.Parse));
            for (int i = 0; i < integers[1]; i++)
            {
                stack.Pop();
            }

            if (stack.Count <= 0)
            {
                Console.WriteLine(0);
            }
            else
            {
                Console.WriteLine(stack.Contains(integers[2]) ? "true" : $"{stack.Min()}");
            }
        }
    }
}
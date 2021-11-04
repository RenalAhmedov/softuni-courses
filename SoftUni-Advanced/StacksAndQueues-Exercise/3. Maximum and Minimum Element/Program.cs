using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Numerics;

namespace _3._Maximum_and_Minimum_Element
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine()); 
            Stack<int> stack = new Stack<int>();

            for (int i = 0; i < n; i++)
            {
                int[] array = Console.ReadLine().Split().Select(int.Parse).ToArray();

                int pushCommand = array[0];

                if (pushCommand == 1)
                {
                    int pushNumber = array[1];
                    stack.Push(pushNumber);
                }
                else if (pushCommand == 2)
                {
                    stack.Pop();
                }
                else if (pushCommand == 3)
                {
                    if (stack.Count <= 0)
                    {
                        continue;
                    }
                    else
                    {
                        int stackmax = stack.Max();
                        Console.WriteLine(stackmax);
                    }
                }
                else if (pushCommand == 4 )
                {
                    if (stack.Count <= 0)
                    {
                        continue;
                    }
                    else
                    {
                        int stackmin = stack.Min();
                        Console.WriteLine(stackmin);
                    }
                }

            }

            Console.WriteLine(string.Join(", ", stack));      
        }
    }
}

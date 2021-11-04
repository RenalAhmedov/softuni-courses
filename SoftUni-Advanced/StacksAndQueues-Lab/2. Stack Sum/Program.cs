using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Numerics;

namespace _2._Stack_Sum
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack<int> collector = new Stack<int>(Console.ReadLine().Split().Select(int.Parse));

            string[] command = Console.ReadLine().ToLower().Split();
            while (command[0] != "end")
            {
                switch (command[0])
                {
                    case "add":
                        collector.Push(int.Parse(command[1]));
                        collector.Push(int.Parse(command[2]));
                        break;
                    case "remove":
                        int n = int.Parse(command[1]);
                        if (n <= collector.Count)
                        {
                            for (int i = 0; i < n; i++)
                            {
                                collector.Pop();
                            }
                        }

                        break;
                }
                command = Console.ReadLine().ToLower().Split();
            }

            Console.WriteLine($"Sum: {collector.Sum()}");

        }
    }
}

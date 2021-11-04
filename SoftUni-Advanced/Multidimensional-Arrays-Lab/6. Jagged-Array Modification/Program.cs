using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Numerics;

namespace _6._Jagged_Array_Modification
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int[][] jaggedArray = new int[n][];
            for (int i = 0; i < n; i++)
            {
                jaggedArray[i] = Console.ReadLine().Split().Select(int.Parse).ToArray();
            }

            string[] command = Console.ReadLine().Split();
            while (command[0] != "END")
            {
                switch (command[0])
                {
                    case "Add":
                        int row = int.Parse(command[1]);
                        int col = int.Parse(command[2]);
                        int add = int.Parse(command[3]);
                        if (row >= 0 && row < jaggedArray.Length && col >= 0 && col < jaggedArray[row].Length)
                        {
                            jaggedArray[row][col] += add;
                        }
                        else
                        {
                            Console.WriteLine("Invalid coordinates");
                        }

                        break;
                    case "Subtract":
                        row = int.Parse(command[1]);
                        col = int.Parse(command[2]);
                        int sub = int.Parse(command[3]);
                        if (row >= 0 && row < jaggedArray.Length && col >= 0 && col < jaggedArray[row].Length)
                        {
                            jaggedArray[row][col] -= sub;
                        }
                        else
                        {
                            Console.WriteLine("Invalid coordinates");
                        }

                        break;
                }
                command = Console.ReadLine().Split();
            }

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(string.Join(" ", jaggedArray[i]));
            }
        }
    }
}

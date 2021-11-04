using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Numerics;

namespace _02._Sets_of_Elemens
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] size = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int n = size[0];
            int m = size[1];

            HashSet<int> leftSet = new HashSet<int>();
            HashSet<int> rightSet = new HashSet<int>();

            for (int i = 0; i < n; i++)
            {
                int currentNumber = int.Parse(Console.ReadLine());
                leftSet.Add(currentNumber);
            }
            for (int i = 0; i < m; i++)
            {
                int currentNumber = int.Parse(Console.ReadLine());
                rightSet.Add(currentNumber);
            }
            List<int> numbers = leftSet.Intersect(rightSet).ToList();

            Console.WriteLine(string.Join(" ", numbers));
        }
    }
}

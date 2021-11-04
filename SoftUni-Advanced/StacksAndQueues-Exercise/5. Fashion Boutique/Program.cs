using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Numerics;

namespace _05._Fashion_Boutique
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack<int> stack = new Stack<int>(Console.ReadLine().Split().Select(int.Parse));
            int capacity = int.Parse(Console.ReadLine());

            int currentRackClothes = 0;
            int rackCounter = 1;

            while (stack.Count > 0)
            {
                if (currentRackClothes + stack.Peek() > capacity)
                {
                    currentRackClothes = 0;
                    rackCounter++;
                }
                currentRackClothes += stack.Pop();
            }

            Console.WriteLine(rackCounter);


        }
    }
}

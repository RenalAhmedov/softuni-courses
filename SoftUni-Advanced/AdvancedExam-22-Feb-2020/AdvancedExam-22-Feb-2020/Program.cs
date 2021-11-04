using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Numerics;
using System.Collections;

namespace AdvancedExam_22_Feb_2020
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] firstLootBox = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int[] secondLootBox = Console.ReadLine().Split().Select(int.Parse).ToArray();

            Queue<int> firstBox = new Queue<int>(firstLootBox);
            Stack<int> secondBox = new Stack<int>(secondLootBox);
            List<int> items = new List<int>();

            while (true)
            {
                int currentIntem = firstBox.Peek() + secondBox.Peek();

                if (currentIntem % 2 == 0)
                {
                    items.Add(currentIntem);
                    firstBox.Dequeue();
                    secondBox.Pop();
                }
                else
                {
                    firstBox.Enqueue(secondBox.Pop());
                }

                if (firstBox.Count <= 0)
                {
                    Console.WriteLine("First lootbox is empty");
                    break;
                }
                if (secondBox.Count <= 0)
                {
                    Console.WriteLine("Second lootbox is empty");
                    break;
                }
            }
            if (items.Sum() >= 100)
            {
                Console.WriteLine($"Your loot was epic! Value: {items.Sum()}");
            }
            else
            {
                Console.WriteLine($"Your loot was poor... Value: {items.Sum()}");
            }
        }
    }
}

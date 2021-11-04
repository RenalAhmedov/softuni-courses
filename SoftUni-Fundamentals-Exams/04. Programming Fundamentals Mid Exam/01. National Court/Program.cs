using System;

namespace _01._National_Court
{
    class Program
    {
        static void Main(string[] args)
        {
            long firstEfficiency = long.Parse(Console.ReadLine());
            long secondEfficiency = long.Parse(Console.ReadLine());
            long thirdEfficiency = long.Parse(Console.ReadLine());

            long totalHours = long.Parse(Console.ReadLine());
            long before = totalHours;

            long time = 0;

            long shiftHours = firstEfficiency + secondEfficiency + thirdEfficiency;

            for (int i = 1; i < before + 1; i++)
            {
                if (i % 4 != 0)
                {
                    if (shiftHours >= totalHours)
                    {
                        time = i;
                        break;
                    }
                    totalHours -= shiftHours;
                }
            }
            Console.WriteLine($"Time needed: {time}h.");
        }
    }   
}

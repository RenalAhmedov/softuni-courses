using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Numerics;

namespace _8._Traffic_Jam
{
    class Program
    {
        static void Main(string[] args)
        {
            int numOfCarsToPass = int.Parse(Console.ReadLine());
            Queue<string> cars = new Queue<string>();
            int passed = 0;

            while (true)
            {
                string line = Console.ReadLine();
                if (line == "end")
                {
                    break;
                }
                else if (line == "green")
                {
                    for (int i = 0; i < numOfCarsToPass; i++)
                    {
                        if (cars.Count > 0)
                        {
                            var car = cars.Dequeue();
                            Console.WriteLine(car + " passed!");
                            passed++;
                        }

                    }
                }
                else
                {
                    var car = line;
                    cars.Enqueue(line);
                }
            }
            Console.WriteLine(passed + " cars passed the crossroads.");
        }
    }
}

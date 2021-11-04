using System;
using System.Collections.Generic;

namespace _6.Parking_Lot
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<string> cars = new HashSet<string>();
            string command = Console.ReadLine();
            while (true)
            {
                string[] commandArgs = command.Split(",");
                string action = commandArgs[0];
                
                if (action == "END")
                {
                    break;
                }
                if (action == "IN")
                {
                    string carPlate = commandArgs[1];
                    cars.Add(carPlate);
                }
                else
                {
                    string carPlate = commandArgs[1];
                    cars.Remove(carPlate);
                }
                if (cars.Count == 0)
                {
                    Console.WriteLine("Parking Lot is Empty");
                }

                command = Console.ReadLine();
            }
            foreach (var car in cars)
            {
                Console.WriteLine(car);
            }
        }
    }
}

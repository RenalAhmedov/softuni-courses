using System;
using System.Collections.Generic;
using System.Linq;

namespace _03_Need_for_Speed_III
{
    class Program
    {
        static void Main()
        {
            int numberOfcars = int.Parse(Console.ReadLine());

            Dictionary<string, List<long>> userMeals = new Dictionary<string, List<long>>();

            for (int i = 0; i < numberOfcars; i++)
            {
                string car = Console.ReadLine();
                string[] words = car.Split('|');

                string carName = words[0];
                long distance = long.Parse(words[1]);
                long fuel = long.Parse(words[2]);

                userMeals.Add(carName, new List<long>());
                userMeals[carName].Add(distance);
                userMeals[carName].Add(fuel);
            }
            while (true)
            {
                string command = Console.ReadLine();

                if (command is "Stop")
                {
                    break;
                }
                string[] newCommand = command.Split(" : ");

                if (newCommand[0] is "Drive")
                {
                    string carName = newCommand[1];
                    long distance = long.Parse(newCommand[2]);
                    long fuel = long.Parse(newCommand[3]);

                    var currentCar = userMeals[carName];

                    if (currentCar[1] >= fuel)
                    {
                        currentCar[1] -= fuel;
                        currentCar[0] += distance;

                        Console.WriteLine($"{carName} driven for { distance } kilometers." +
                                          $" { fuel} liters of fuel consumed.");
                    }
                    else
                    {
                        Console.WriteLine("Not enough fuel to make that ride");
                    }

                    if (currentCar[0] >= 100000)
                    {

                        userMeals.Remove(carName);

                        Console.WriteLine($"Time to sell the {carName}!");
                    }
                }
                else if (newCommand[0] is "Refuel")
                {
                    string carName = newCommand[1];
                    long fuel = long.Parse(newCommand[2]);

                    List<long> currentCar = userMeals[carName];

                    long before = currentCar[1];

                    currentCar[1] += fuel;

                    if (currentCar[1] > 75)
                    {
                        currentCar[1] = 75;
                    }

                    Console.WriteLine($"{carName} refueled with {currentCar[1] - before} liters");
                }
                else if (newCommand[0] is "Revert")
                {
                    string carName = newCommand[1];
                    long distance = long.Parse(newCommand[2]);

                    List<long> currentCar = userMeals[carName];

                    currentCar[0] -= distance;

                    if (currentCar[0] < 10000)
                    {
                        currentCar[0] = 10000;
                    }
                    else
                    {
                        Console.WriteLine($"{carName} mileage decreased by {distance} kilometers");
                    }
                }
            }

            var sortedCollection = userMeals.OrderByDescending(m => m.Value[0]).ThenBy(n => n.Key);

            foreach ((string name, List<long> meals) in sortedCollection)
            {
                Console.WriteLine($"{name} -> Mileage: {meals[0]} kms, Fuel in the tank: {meals[1]} lt.");
            }
        }
    }
}
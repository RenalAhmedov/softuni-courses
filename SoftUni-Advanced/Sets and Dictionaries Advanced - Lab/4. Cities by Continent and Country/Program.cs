using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Numerics;

namespace _4._Cities_by_Continent_and_Country
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Dictionary<string, Dictionary<string, List<string>>> dictionary = new Dictionary<string, Dictionary<string, List<string>>>();
            string command = Console.ReadLine();

            for (int i = 0; i < n; i++)
            {
                string[] commandArgs = command.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();
                string continent = commandArgs[0];
                string country = commandArgs[1];
                string city = commandArgs[2];

                if (dictionary.ContainsKey(continent) == false)
                {
                    dictionary.Add(continent, new Dictionary<string, List<string>>());
                }
                if (dictionary[continent].ContainsKey(country) == false)
                {
                    dictionary[continent].Add(country, new List<string>());
                }
                dictionary[continent][country].Add(city);

                command = Console.ReadLine();
            }

            foreach (var continent in dictionary)
            {
                Console.WriteLine($"{continent.Key}:");

                foreach (var country in continent.Value)
                {
                    Console.WriteLine($"  {country.Key} -> {string.Join(", ", country.Value)}");
                }
            }
        }
    }
}

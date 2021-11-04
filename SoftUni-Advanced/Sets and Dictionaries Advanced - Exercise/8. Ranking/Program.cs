using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Numerics;

namespace _8._Ranking
{
    class Program
    {
        static void Main(string[] args)
        {
            var contests = new Dictionary<string, string>();
            var contestsResults = new Dictionary<string, Dictionary<string, int>>();
            string zero = "";

            string command = Console.ReadLine();
            while (command != "end of contests")
            {
                string[] commandArgs = command.Split(":").ToArray();
                string contestName = commandArgs[0];
                string contestPassword = commandArgs[1];

                if (contests.ContainsKey(contestName) == false)
                {
                    contests.Add(contestName, zero);
                }
                contests[contestName] += contestPassword;

                command = Console.ReadLine();
            }

            string input = Console.ReadLine();
            while (input != "end of submissions")
            {
                string[] inputArgs = input.Split("=>").ToArray();
                string contestName = inputArgs[0];
                string contestPassword = inputArgs[1];
                string person = inputArgs[2];
                int points = int.Parse(inputArgs[3]);

                if (contests.ContainsKey(contestName) && contests.ContainsValue(contestPassword))
                {
                    if (contestsResults.ContainsKey(person) && contestsResults[person].ContainsKey(contestName))
                    {
                        if (contestsResults[person][contestName] < points)
                        {
                            contestsResults[person][contestName] = points;
                            //check if gurmi
                            input = Console.ReadLine();
                            continue;
                        }
                        else
                        {
                            input = Console.ReadLine();
                            continue;
                        }
                        
                    }
                    if (contestsResults.ContainsKey(person))
                    {
                        contestsResults[person].Add(contestName, points);
                        
                    }
                    else
                    {
                        contestsResults.Add(person, new Dictionary<string, int>());
                        contestsResults[person].Add(contestName, points);
                        
                    }
                  
                }

                input = Console.ReadLine();
            }

            string bestCand = string.Empty;
            int highestPoints = 0;
            foreach (var student in contestsResults)
            {
                int sum = 0;
                foreach (var contest in student.Value)
                {
                    sum += contest.Value;
                }
                if (sum > highestPoints)
                {
                    bestCand = student.Key;
                    highestPoints = sum;
                }
            }
            Console.WriteLine($"Best candidate is {bestCand} with total {highestPoints} points.");

            contestsResults = contestsResults.OrderBy(s => s.Key).ToDictionary(s => s.Key, s => s.Value);
            Console.WriteLine("Ranking: ");
            foreach (var student in contestsResults)
            {
                Console.WriteLine(student.Key);
                foreach (var contest in student.Value.OrderByDescending(s => s.Value))
                {
                    Console.WriteLine($"#  {contest.Key} -> {contest.Value}");
                }
            }
        }      
    }
}

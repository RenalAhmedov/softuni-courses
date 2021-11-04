using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonInfo
{
    public class StartUp
    {
        static void Main(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            List<Citizen> list = new List<Citizen>();
            List<Rebel> listTwo = new List<Rebel>();

            for (int i = 0; i < n; i++)
            {
                string[] personDetails = Console.ReadLine().Split().ToArray();
                if (personDetails.Length <= 3)
                {
                    string name = personDetails[0];
                    int age = int.Parse(personDetails[1]);
                    string group = personDetails[2];
                    listTwo.Add(new Rebel(name, age, group));
                }
                else
                {
                    list.Add(new Citizen(personDetails[0], int.Parse(personDetails[1]), personDetails[2], personDetails[3]));
                }

            }

            string input = Console.ReadLine();
            while (input != "End")
            {
                
                if (list.Any(x => x.Name == input))
                {
                  list.FirstOrDefault(c => c.Name == input).BuyFood();
                }
                else if (listTwo.Any(x => x.Name == input))
                {
                    listTwo.FirstOrDefault(c => c.Name == input).BuyFood();
                }

                input = Console.ReadLine();
            }

            Console.WriteLine(listTwo.Sum(r => r.Food) + list.Sum(c => c.Food));
        }
    }
}
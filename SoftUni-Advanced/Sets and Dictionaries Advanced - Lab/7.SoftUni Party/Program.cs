using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Numerics;

namespace _7.SoftUni_Party
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<string> vipGuests = new HashSet<string>();
            HashSet<string> regularGuests = new HashSet<string>();
            int counter = 0;
            bool partyStarted = false;

            string input = Console.ReadLine();

            while (input != "END")
            {
                if (input == "PARTY")
                {
                    partyStarted = true;
                }
                if (partyStarted)
                {
                    input = Console.ReadLine();
                    if (vipGuests.Contains(input))
                    {
                        vipGuests.Remove(input);
                    }
                    if (regularGuests.Contains(input))
                    {
                        regularGuests.Remove(input);
                    }
                    
                    continue;

                }

                if (vipGuests.Contains(input) == false)
                {
                    if (char.IsDigit(input[0]))
                    {
                        vipGuests.Add(input);
                    }

                }
                if (regularGuests.Contains(input) == false)
                {
                    if (char.IsLetter(input[0]))
                    {
                        regularGuests.Add(input);

                    }
                }
               
                input = Console.ReadLine();

            }
            Console.WriteLine(vipGuests.Count + regularGuests.Count);
            foreach (var g in vipGuests)
            {
                Console.WriteLine(g);
            }
            foreach (var g in regularGuests)
            {
                Console.WriteLine(g);
            }

        }
    }
}

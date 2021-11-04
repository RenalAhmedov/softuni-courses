using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace _02_Mirror_Words
{
    class Program
    {
        static void Main()
        {
            string input = Console.ReadLine();

            string pattern = @"[@]{1}[A-z]{3,}[@]{1}[@]{1}[A-z]{3,}[@]{1}|[#]{1}[A-z]{3,}[#]{1}[#]{1}[A-z]{3,}[#]{1}";

            RegexOptions options = RegexOptions.IgnoreCase;

            MatchCollection list = Regex.Matches(input, pattern, options);

            int totalCounter = list.Count;


            List<string> first = new List<string>();
            List<string> secound = new List<string>();


            for (int i = 0; i < list.Count; i++)
            {
                Match match = list[i];
                string pattern2 = @"[@,#]{1}[A-z]{3,}[@,#]{1}";
                string input2 = match.Value;
                RegexOptions options2 = RegexOptions.IgnoreCase;

                MatchCollection list2 = Regex.Matches(input2, pattern2, options2);

                static string ReverseString(string s)
                {
                    char[] arr = s.ToCharArray();
                    Array.Reverse(arr);
                    return new string(arr);
                }

                string firstValue = list2[0].Value;
                string secoundValue = ReverseString(list2[1].Value);

                if (firstValue == secoundValue)
                {
                    first.Add(firstValue.Substring(1, firstValue.Length - 2));
                    secound.Add(list2[1].Value.Substring(1, list2[1].Value.Length - 2));
                }
            }

            if (totalCounter > 0)
            {
                Console.WriteLine($"{totalCounter} word pairs found!");

                if (first.Count > 0)
                {
                    Console.WriteLine("The mirror words are:");

                    for (int i = 0; i < first.Count; i++)
                    {
                        Console.Write($"{first[i]} <=> {secound[i]}");
                        if (i < first.Count - 1)
                        {
                            Console.Write(", ");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No mirror words!");
                }

            }
            else
            {
                Console.WriteLine("No word pairs found!");
                Console.WriteLine("No mirror words!");
            }
        }
    }
}
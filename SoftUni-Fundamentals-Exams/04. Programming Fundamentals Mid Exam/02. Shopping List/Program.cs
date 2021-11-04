using System;
using System.Linq;
using System.Collections.Generic;

namespace _02._Shopping_List
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> list = Console.ReadLine().Split("!").ToList();

            string command = Console.ReadLine();

            while (command != "Go Shopping!")
            {
                string[] commandArgs = command.Split();

                if (commandArgs[0] == "Urgent")
                {
                    if (list.Contains(commandArgs[1]))
                    {

                    }
                    else
                    {
                        list.Insert(0, commandArgs[1]);
                    }

                }
                else if (commandArgs[0] == "Unnecessary")
                {
                    if (list.Contains(commandArgs[1]))
                    {
                        list.Remove(commandArgs[1]);
                    }
                }
                else if (commandArgs[0] == "Correct")
                {
                    if (list.Contains(commandArgs[1]))
                    {
                        list[list.FindIndex(ind => ind.Equals(commandArgs[1]))] = commandArgs[2];
                    }
                }
                else if (commandArgs[0] == "Rearrange")
                {
                    if (list.Contains(commandArgs[1]))
                    {
                        var x = list.Find(x => x.Equals(commandArgs[1]));

                        list.Remove(x);
                        list.Add(x);
                    }
                }
                command = Console.ReadLine();
            }
            for (int i = 0; i < list.Count; i++)
            {

                if (i < list.Count - 1)
                {
                    Console.Write(list[i] + ", ");
                }
                else
                {
                    Console.Write(list[i]);
                }
            }
        }
    }
}

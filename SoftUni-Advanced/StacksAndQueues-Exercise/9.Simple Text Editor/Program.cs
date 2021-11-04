using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Numerics;

namespace _9.Simple_Text_Editor
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            StringBuilder sb = new StringBuilder();
            Stack<string> stringStates = new Stack<string>();

            for (int i = 0; i < n; i++)
            {
                string[] actionParams = Console.ReadLine().Split();

                string action = actionParams[0];

                if (action == "1")
                {
                    stringStates.Push(sb.ToString());
                    string value = actionParams[1];
                    sb.Append(value);
                }
                else if (action == "2")
                {
                    stringStates.Push(sb.ToString());
                    int count = int.Parse(actionParams[1]);

                    while (count > 0)
                    {
                        sb.Remove(sb.Length - 1, 1);
                        count--;
                    }
                }
                else if (action == "3")
                {
                    int elementNumber = int.Parse(actionParams[1]);
                    Console.WriteLine(sb[elementNumber - 1]);
                }
                else
                {
                    sb.Clear();
                    sb.Append(stringStates.Pop());
                }
            }
        }
    }
}

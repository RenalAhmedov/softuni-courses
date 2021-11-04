using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Numerics;

namespace _3.Simple_Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack<string> elements = new Stack<string>(Console.ReadLine().Split().Reverse());
            while (elements.Count > 1)
            {
                int a = int.Parse(elements.Pop());
                string @operator = elements.Pop();
                int b = int.Parse(elements.Pop());
                elements.Push(@operator == "+" ? (a + b).ToString() : (a - b).ToString());
            }

            Console.WriteLine(elements.Pop());
        }
    }
}

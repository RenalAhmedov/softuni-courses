using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Numerics;

namespace _4.Matching_Brackets
{
    class Program
    {
        static void Main(string[] args)
        {
            string expression = Console.ReadLine();
            Stack<int> indices = new Stack<int>();
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '(')
                {
                    indices.Push(i);
                }
                else if (expression[i] == ')' )
                {
                    var openBracketIndex = indices.Pop();
                    var closedBracketsIndex = i;
                    Console.WriteLine(expression.Substring(openBracketIndex, closedBracketsIndex - openBracketIndex +1));
                }
            }
        }
    }
}

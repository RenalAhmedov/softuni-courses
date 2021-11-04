using System;
using System.Linq;

namespace _3.UpperWords
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Where(c => char.IsUpper(c[0])).ToList().ForEach(Console.WriteLine);
        }
    }
}

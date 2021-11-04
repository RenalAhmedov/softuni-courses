using System;
using System.Collections.Generic;
using System.Linq;

namespace _05.Applied_Arithmetics
{
    class Program
    {
        static void Main(string[] args)
        {
            int nameLength = int.Parse(Console.ReadLine());
            string[] names = Console.ReadLine().Split().Where(x => x.Length <= nameLength).ToArray();
            foreach (var name in names)
            {
                Console.WriteLine(string.Join(" ", name));
            }
        }
    }
}

using System;

namespace Stealer
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Spy currentSpy = new Spy();
            string result = currentSpy.RevealPrivateMethods("Stealer.Hacker");
            Console.WriteLine(result);
        }
    }
}
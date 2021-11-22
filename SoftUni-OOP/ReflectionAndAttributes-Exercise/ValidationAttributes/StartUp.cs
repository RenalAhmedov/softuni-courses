using System;

namespace ValidationAttributes
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var person = new Person
             (
                 "Renal",
                 21
             );

            bool isValidEntity = Validator.IsValid(person);

            Console.WriteLine(isValidEntity);
        }
    }
}
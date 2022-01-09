using System;

namespace Voleyball
{
    class Program
    {
        static void Main(string[] args)
        {
            string yearType = Console.ReadLine();
            double holidays = int.Parse(Console.ReadLine());
            double weekendsInHomeTown = int.Parse(Console.ReadLine());

            double weekends = 48;
            double holidayGames = holidays * 2 / 3;
            double sofiaGames = (weekends - weekendsInHomeTown) * 3 / 4;

            double totalGames = holidayGames + sofiaGames + weekendsInHomeTown;

            switch (yearType)
            {
                case "leap":
                    totalGames *= 1.15;
                    break;
            }

            Console.WriteLine($"{Math.Floor(totalGames)}");
        }
    }
}
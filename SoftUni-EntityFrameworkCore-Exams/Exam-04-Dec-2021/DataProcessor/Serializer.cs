namespace Theatre.DataProcessor
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Theatre.Data;
    using Theatre.DataProcessor.ExportDto;

    public class Serializer
    {
        public static string ExportTheatres(TheatreContext context, int numbersOfHalls)
        {
            var theatres = context.Theatres
                .ToArray()
                .Where(x => x.NumberOfHalls >= numbersOfHalls && x.Tickets.Count >= 20)
                .Select(x => new
                {
                    Name = x.Name,
                    Halls = x.NumberOfHalls,
                    TotalIncome = x.Tickets
                    .Where(t => t.RowNumber >= 1 && t.RowNumber <= 5)
                    .Sum(t => t.Price),
                    Tickets = x.Tickets
                    .ToArray()
                    .Where(t => t.RowNumber >= 1 && t.RowNumber <= 5)
                    .Select(t => new
                    {
                        Price = t.Price,
                        RowNumber = t.RowNumber
                    })
                    .OrderByDescending(t => t.Price)
                    .ToArray()
                })
                .OrderByDescending(x => x.Halls)
                .ThenBy(x => x.Name)
                .ToArray();

            var json = JsonConvert.SerializeObject(theatres, Formatting.Indented);

            return json;
        }

        public static string ExportPlays(TheatreContext context, double rating)
        {
            var serializer = new XmlSerializer(typeof(List<PlayDto>), new XmlRootAttribute("Plays"));
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var plays = context.Plays
                .ToList()
                .Where(x => x.Rating <= rating)
                .Select(x => new PlayDto
                {
                    Title = x.Title,
                    Duration = x.Duration.ToString("c"),
                    Rating = x.Rating == 0 ? "Premier" : x.Rating.ToString(),
                    Genre = x.Genre.ToString(),
                    Actors = x.Casts
                    .ToList()
                    .Where(a => a.IsMainCharacter == true)
                    .Select(a => new ActorDto
                    {
                        FullName = a.FullName,
                        MainCharacter = $"Plays main character in '{x.Title}'."
                    })
                    .OrderByDescending(a => a.FullName)
                    .ToList()
                })
                .OrderBy(x => x.Title)
                .ThenByDescending(x => x.Genre)
                .ToList();

            serializer.Serialize(sw, plays, namespaces);

            return sb.ToString();
        }
    }
}
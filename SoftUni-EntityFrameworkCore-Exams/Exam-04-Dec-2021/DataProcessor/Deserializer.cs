namespace Theatre.DataProcessor
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Theatre.Data;
    using Theatre.Data.Models;
    using Theatre.Data.Models.Enums;
    using Theatre.DataProcessor.ImportDto;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfulImportPlay
            = "Successfully imported {0} with genre {1} and a rating of {2}!";

        private const string SuccessfulImportActor
            = "Successfully imported actor {0} as a {1} character!";

        private const string SuccessfulImportTheatre
            = "Successfully imported theatre {0} with #{1} tickets!";

        public static string ImportPlays(TheatreContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(PlayInputModel[]), new XmlRootAttribute("Plays"));
            var sb = new StringBuilder();

            var plays = new HashSet<Play>();
            var playDtos = (PlayInputModel[])serializer.Deserialize(new StringReader(xmlString));

            foreach (var playDto in playDtos)
            {
                if (!IsValid(playDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (!Enum.IsDefined(typeof(Genre), playDto.Genre))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var isValidDuration = TimeSpan.TryParse(playDto.Duration, out var newDuration);

                if (!isValidDuration)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (newDuration.Hours < 1)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var play = new Play
                {
                    Title = playDto.Title,
                    Duration = newDuration,
                    Rating = playDto.Rating,
                    Genre = Enum.Parse<Genre>(playDto.Genre),
                    Description = playDto.Description,
                    Screenwriter = playDto.Screenwriter
                };

                plays.Add(play);

                sb.AppendFormat(SuccessfulImportPlay, play.Title, play.Genre, play.Rating);
                sb.AppendLine();
            }

            context.Plays.AddRange(plays);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportCasts(TheatreContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(CastInputModel[]), new XmlRootAttribute("Casts"));
            var sb = new StringBuilder();

            var castDtos = (CastInputModel[])serializer.Deserialize(new StringReader(xmlString));
            var casts = new HashSet<Cast>();

            foreach (var castDto in castDtos)
            {
                if (!IsValid(castDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var cast = new Cast
                {
                    FullName = castDto.FullName,
                    IsMainCharacter = castDto.IsMainCharacter,
                    PhoneNumber = castDto.PhoneNumber,
                    PlayId = castDto.PlayId
                };

                casts.Add(cast);

                sb.AppendFormat(SuccessfulImportActor, cast.FullName, cast.IsMainCharacter ? "main" : "lesser");
                sb.AppendLine();
            }

            context.Casts.AddRange(casts);
            context.SaveChanges();

            return sb.ToString();
        }
        public static string ImportTtheatersTickets(TheatreContext context, string jsonString)
        {
            var theatreDtos = JsonConvert.DeserializeObject<TheatreInputModel[]>(jsonString).ToArray();
            var sb = new StringBuilder();

            var theatres = new HashSet<Theatre>();

            foreach (var theatreDto in theatreDtos)
            {
                if (!IsValid(theatreDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var theatre = new Theatre
                {
                    Name = theatreDto.Name,
                    NumberOfHalls = theatreDto.NumberOfHalls,
                    Director = theatreDto.Director
                };

                foreach (var ticketDto in theatreDto.Tickets)
                {
                    if (!IsValid(ticketDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var ticket = new Ticket
                    {
                        Price = ticketDto.Price,
                        RowNumber = ticketDto.RowNumber,
                        PlayId = ticketDto.PlayId
                    };

                    theatre.Tickets.Add(ticket);
                }

                theatres.Add(theatre);

                sb.AppendFormat(SuccessfulImportTheatre, theatre.Name, theatre.Tickets.Count);
                sb.AppendLine();
            }

            context.Theatres.AddRange(theatres);
            context.SaveChanges();

            return sb.ToString();
        }


        private static bool IsValid(object obj)
        {
            var validator = new ValidationContext(obj);
            var validationRes = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validator, validationRes, true);
            return result;
        }
    }
}

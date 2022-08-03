namespace VaporStore.DataProcessor
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.Data.Models;
    using VaporStore.Data.Models.Enums;
    using VaporStore.DataProcessor.Dto.Import;

    public static class Deserializer
	{
		public static string ImportGames(VaporStoreDbContext context, string jsonString)
		{
			var sb = new StringBuilder();
			var gameDtos = JsonConvert.DeserializeObject<ImportGamesJsonDtos[]>(jsonString);

			var games = new HashSet<Game>();

			foreach (var gameDto in gameDtos)
			{
				if (!IsValid(gameDto) || !gameDto.Tags.Any())
				{
					sb.AppendLine("Invalid Data");
					continue;
				}

				var isValidDate = DateTime.TryParseExact(gameDto.ReleaseDate, "yyyy-MM-dd",
					CultureInfo.InvariantCulture, DateTimeStyles.None, out var newReleaseDate);

				if (!isValidDate)
				{
					sb.AppendLine("Invalid Data");
					continue;
				}

				var developer = context.Developers.FirstOrDefault(x => x.Name == gameDto.Developer);
				var genre = context.Genres.FirstOrDefault(x => x.Name == gameDto.Genre);

				if (developer == null)
				{
					developer = new Developer
					{
						Name = gameDto.Developer
					};
				}

				if (genre == null)
				{
					genre = new Genre
					{
						Name = gameDto.Genre
					};
				}

				var game = new Game
				{
					Name = gameDto.Name,
					Price = gameDto.Price,
					ReleaseDate = newReleaseDate,
					Developer = developer,
					Genre = genre
				};

				foreach (var tagName in gameDto.Tags)
				{
					var tag = context.Tags.FirstOrDefault(x => x.Name == tagName);

					if (tag == null)
					{
						tag = new Tag
						{
							Name = tagName
						};
					}

					game.GameTags.Add(new GameTag
					{
						Tag = tag,
						Game = game
					});
				}

				context.Games.Add(game);
				context.SaveChanges();

				sb.AppendLine($"Added {game.Name} ({gameDto.Genre}) with {game.GameTags.Count} tags");
			}

			return sb.ToString();
		}

		public static string ImportUsers(VaporStoreDbContext context, string jsonString)
		{
			var sb = new StringBuilder();

			var userDtos = JsonConvert.DeserializeObject<ImportUsersJsonDtos[]>(jsonString);
			var users = new HashSet<User>();

			foreach (var userDto in userDtos)
			{
				if (!IsValid(userDto) || userDto.Cards.Count == 0)
				{
					sb.AppendLine("Invalid Data");
					continue;
				}

				var user = new User
				{
					FullName = userDto.FullName,
					Username = userDto.Username,
					Email = userDto.Email,
					Age = userDto.Age
				};

				var isValidCard = true;

				foreach (var cardDto in userDto.Cards)
				{
					if (!IsValid(cardDto) || !Enum.IsDefined(typeof(CardType), cardDto.Type))
					{
						sb.AppendLine("Invalid Data");
						isValidCard = false;
						break;
					}

					var card = new Card
					{
						Number = cardDto.Number,
						CVC = cardDto.CVC,
						Type = Enum.Parse<CardType>(cardDto.Type)
					};

					user.Cards.Add(card);
				}

				if (!isValidCard)
				{
					continue;
				}

				users.Add(user);

				sb.AppendLine($"Imported {user.Username} with {user.Cards.Count} cards");
			}

			context.Users.AddRange(users);
			context.SaveChanges();

			return sb.ToString();
		}

		public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
		{
			var serializer = new XmlSerializer(typeof(ImportPurchaseXmlDtos[]), new XmlRootAttribute("Purchases"));
			var sb = new StringBuilder();

			var purchaseDtos = (ImportPurchaseXmlDtos[])serializer.Deserialize(new StringReader(xmlString));
			var purchases = new HashSet<Purchase>();

            foreach (var purchaseDto in purchaseDtos)
            {
				if (!IsValid(purchaseDto) || !Enum.IsDefined(typeof(PurchaseType), purchaseDto.Type))
				{
					sb.AppendLine("Invalid Data");
					continue;
				}

				var isValidDate = DateTime.TryParseExact(purchaseDto.Date, "dd/MM/yyyy HH:mm",
					CultureInfo.InvariantCulture, DateTimeStyles.None, out var newDate);

				if (!isValidDate)
				{
					sb.AppendLine("Invalid Data");
					continue;
				}

				var purchase = new Purchase
				{
					Type = Enum.Parse<PurchaseType>(purchaseDto.Type),
					ProductKey = purchaseDto.Key,
					Card = context.Cards.FirstOrDefault(x => x.Number == purchaseDto.Card),
					Date = newDate,
					Game = context.Games.FirstOrDefault(x => x.Name == purchaseDto.Title)

				};
				purchases.Add(purchase);
				sb.AppendLine($"Imported {purchase.Game.Name} for {purchase.Card.User.Username}");
			}
			context.Purchases.AddRange(purchases);
			context.SaveChanges();

			return sb.ToString();
		}

		private static bool IsValid(object dto)
		{
			var validationContext = new ValidationContext(dto);
			var validationResult = new List<ValidationResult>();

			return Validator.TryValidateObject(dto, validationContext, validationResult, true);
		}
	}
}
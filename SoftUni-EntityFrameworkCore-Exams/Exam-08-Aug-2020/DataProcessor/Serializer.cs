namespace VaporStore.DataProcessor
{
	using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.DataProcessor.ExportDto;

    public static class Serializer
	{
		public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
		{
			var games = context.Genres
				.ToArray()
				.Where(x => genreNames.Contains(x.Name))
				.Select(x => new
				{
					Id = x.Id,
					Genre = x.Name,
					Games = x.Games
					.ToArray()
					.Select(g => new
					{
						Id = g.Id,
						Title = g.Name,
						Developer = g.Developer.Name,
						Tags = string.Join(", ", g.GameTags.Select(t => t.Tag.Name)),
						Players = g.Purchases.Count
					})
					.Where(g => g.Players > 0)
					.OrderByDescending(g => g.Players)
					.ThenBy(g => g.Id),
					TotalPlayers = x.Games.Sum(g => g.Purchases.Count)
				})
				.OrderByDescending(x => x.TotalPlayers)
				.ThenBy(x => x.Id)
				.ToArray();

			return JsonConvert.SerializeObject(games, Formatting.Indented);
		}


		public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
		{
			var serializer = new XmlSerializer(typeof(UserOutputModel[]), new XmlRootAttribute("Users"));

			var sb = new StringBuilder();
			var sw = new StringWriter(sb);

			var namespaces = new XmlSerializerNamespaces();
			namespaces.Add(string.Empty, string.Empty);

			var users = context.Users
				.ToArray()
				.Where(x => x.Cards.Any(c => c.Purchases.Any(p => p.Type.ToString() == storeType)))
				.Select(x => new UserOutputModel
				{
					Username = x.Username,
					TotalSpent = x.Cards.Sum(c => c.Purchases.Where(p => p.Type.ToString() == storeType).Sum(s => s.Game.Price)),
					Purchases = x.Cards.SelectMany(x => x.Purchases)
					.Where(x => x.Type.ToString() == storeType)
					.Select(p => new PurchaseOutputModel
					{
						Card = p.Card.Number,
						Cvc = p.Card.CVC,
						Date = p.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
						Game = new GameOutputModel
						{
							Title = p.Game.Name,
							Genre = p.Game.Genre.Name,
							Price = p.Game.Price
						}
					})
					.OrderBy(p => p.Date)
					.ToArray(),
				})
				.OrderByDescending(x => x.TotalSpent)
				.ThenBy(x => x.Username)
				.ToArray();

			serializer.Serialize(sw, users, namespaces);

			return sb.ToString();

		}
	}
}
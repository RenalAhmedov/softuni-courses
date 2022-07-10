namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            DbInitializer.ResetDatabase(db);

            //Console.WriteLine(RemoveBooks(db));
            //IncreasePrices(db);
        }

        //02->
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            StringBuilder sb = new StringBuilder();
            var ageRestriction = Enum.Parse<AgeRestriction>(command, true);

            var books = context.Books
                .Where(x => x.AgeRestriction == ageRestriction)
                .Select(x => x.Title)
                .OrderBy(x => x)
                .ToArray();

            foreach (var book in books)
            {
                sb.AppendLine(book);
            }

            return sb.ToString().Trim();
        }
        //<-

        //03->
        public static string GetGoldenBooks(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var goldenBooks = context.Books
                .Where(x => x.EditionType == EditionType.Gold && x.Copies < 5000)
                .OrderBy(b => b.BookId)
                .Select(x => x.Title)
                .ToArray();

            foreach (var book in goldenBooks)
            {
                sb.AppendLine(book);
            }
            return sb.ToString();
        }
        //<-

        //04->
        public static string GetBooksByPrice(BookShopContext context)
        {
            var booksByPrice = context.Books
                .Where(bp => bp.Price > 40)
                .Select(x => new
                {
                    xTitle = x.Title,
                    xPrice = x.Price
                })
                .OrderByDescending(x => x.xPrice)
                .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var book in booksByPrice)
            {
                sb.AppendLine($"{book.xTitle} - ${book.xPrice:F2}");
            }
            return sb.ToString().Trim();
        }
        //<-

        //05->
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var notReleasedBooks = context.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(x => x.Title )
                .ToArray();

            return string.Join(Environment.NewLine, notReleasedBooks);
        }
        //<-

        //06->
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var categories = input.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

            var books = context.BooksCategories
               .Where(x => categories.Contains(x.Category.Name.ToLower()))
               .Select(x => x.Book.Title)
               .OrderBy(x => x)
               .ToArray();

            var sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine(book);
            }

            return sb.ToString().Trim();
        }
        //<-

        //07->
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var booksReleasedBeforeDate = context.Books
                .Where(b => b.ReleaseDate < DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture))
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => new
                {
                    b.Title,
                    b.EditionType,
                    b.Price
                })
                .ToArray();

            var sb = new StringBuilder();

            foreach (var book in booksReleasedBeforeDate)
            {
                sb.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:f2}");
            }

            return sb.ToString().Trim();
        }
        //<-

        //08->
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authorNameEndingWith = context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(x => x.FirstName + " " + x.LastName)
                .OrderBy(x => x)
                .ToArray();

            return string.Join(Environment.NewLine, authorNameEndingWith).TrimEnd();
        }
        //<-

        //09->
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var bookByTitleContains = context.Books
                .Where(x => x.Title.ToLower().Contains(input.ToLower()))
                .Select(x => x.Title)
                .OrderBy(x => x)
                .ToArray();

            return string.Join(Environment.NewLine, bookByTitleContains);
        }
        //<-

        //10->
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var authorsFound = context.Books
                .Where(x => x.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .Select(x => new
                {
                    x.Title,
                    Author = x.Author.FirstName + " " + x.Author.LastName
                })
                .ToArray();

            var sb = new StringBuilder();

            foreach (var book in authorsFound)
            {
                sb.AppendLine($"{book.Title} ({book.Author})");
            }

            return sb.ToString();


        }
        //<-

        //11->
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            return context.Books.Where(x => x.Title.Length > lengthCheck).Count();

        }
        //<-

        //12->
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var authors = context.Authors.Select(x => new
            {
                FullName = x.FirstName + " " + x.LastName,
                Copies = x.Books.Sum(x => x.Copies)
            })
                .OrderByDescending(x => x.Copies)
                .ToArray();

            var sb = new StringBuilder();

            foreach (var author in authors)
            {
                sb.AppendLine($"{author.FullName} - {author.Copies}");
            }

            return sb.ToString();
        }
        //<-

        //13->
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var categories = context.Categories
                .Select(x => new
                {
                    x.Name,
                    Profit = x.CategoryBooks.Sum(b => b.Book.Price * b.Book.Copies)
                })
                .OrderByDescending(x => x.Profit)
                .ThenBy(x => x.Name)
                .ToArray();


            var sb = new StringBuilder();

            foreach (var category in categories)
            {
                sb.AppendLine($"{category.Name} ${category.Profit:f2}");
            }

            return sb.ToString();
        }
        //<--

        //14->
        public static string GetMostRecentBooks(BookShopContext context)
        {
            var categoriesWithBooks = context.Categories.Select(x => new
            {
                CategoryName = x.Name,
                Books = x.CategoryBooks
                .OrderByDescending(b => b.Book.ReleaseDate)
                .Select(b => new
                {
                    BookTitle = b.Book.Title,
                    ReleaseYear = b.Book.ReleaseDate.Value.Year,
                })
                .Take(3)
            })
                .OrderBy(x => x.CategoryName)
                .ToArray();

            var sb = new StringBuilder();

            foreach (var category in categoriesWithBooks)
            {
                sb.AppendLine($"--{category.CategoryName}");

                foreach (var book in category.Books)
                {
                    sb.AppendLine($"{book.BookTitle} ({book.ReleaseYear})");
                }
            }

            return sb.ToString();
        }
        //<-

        //15->
        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books.Where(x => x.ReleaseDate.Value.Year < 2010)
                .ToArray();

            foreach (var book in books)
            {
                book.Price += 5;
            }

            context.SaveChanges();
        }
        //<-

        //16->
        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books.Where(x => x.Copies < 4200).ToArray();

            foreach (var book in books)
            {
                context.Books.Remove(book);
            }
            context.SaveChanges();

            return books.Count();
        }
        //<-
    }
}

using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations;

public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
        {
            if (context.Books.Any())
            {
                return;
            }
            context.AddRange(
                new Genre
                {
                    Name = "Personal Growth",
                },
                new Genre
                {
                    Name = "Science Fiction",
                },
                new Genre
                {
                    Name = "Romance",
                });
            context.Books.AddRange(

                new Book
                {
                    Title = "Lean Startup",
                    GenreId = 1, //Personal Growth
                    PageCount = 200,
                    PublishDate = new DateTime(2011, 06, 12),
                    AuthorId = 1
                },
                new Book
                {
                    Title = "Herland",
                    GenreId = 2, //Science Fiction
                    PageCount = 250,
                    PublishDate = new DateTime(2010, 05, 23),
                    AuthorId = 2
                },
                new Book
                {
                    Title = "Dune",
                    GenreId = 2, //Science Fiction
                    PageCount = 540,
                    PublishDate = new DateTime(2001, 12, 21),
                    AuthorId = 3
                }
            );
            context.Authors.AddRange(
                new Author
                {
                    Name = "Eric",
                    Surname = "Ries",
                    Birthday = new DateTime(1978, 9, 22),
                },
                new Author
                {
                    Name = "Charlotte Perkins",
                    Surname = "Gilman",
                    Birthday = new DateTime(1860, 7, 3),
                },
                new Author
                {
                    Name = "Frank",
                    Surname = "Herbert",
                    Birthday = new DateTime(1920, 10, 8),
                }
                );
            context.SaveChanges();
        };
    }
}
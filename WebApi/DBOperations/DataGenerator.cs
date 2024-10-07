using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DbOperations;

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
                            PublishDate = new DateTime(2011, 06, 12)
                        },
                        new Book
                        {
                            Title = "Herland",
                            GenreId = 2, //Science Fiction
                            PageCount = 250,
                            PublishDate = new DateTime(2010, 05, 23)
                        },
                        new Book
                        {
                            Title = "Dune",
                            GenreId = 2, //Science Fiction
                            PageCount = 540,
                            PublishDate = new DateTime(2001, 12, 21)
                        }
            );
            context.SaveChanges();
        };
    }
}
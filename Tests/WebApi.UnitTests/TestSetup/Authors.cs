using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup;

public static class Authors
{
    public static void AddAuthors(this BookStoreDbContext context)
    {
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
            },
            new Author
            {
                Name = "test",
                Surname = "test",
                Birthday = new DateTime(1950, 10, 8),
            }
            );
    }
}
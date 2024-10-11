using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApi.Entities;

namespace WebApi.DBOperations;

public class BookStoreDbContext : DbContext, IBookStoreDbContext
{
    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
    {

    }
    public DbSet<Book> Books { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Author> Authors { get; set; }

    public override EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class
    {
        return base.Add(entity);
    }

    public override EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class
    {
        return base.Remove(entity);
    }

    public override int SaveChanges()
    {
        return base.SaveChanges();
    }

}
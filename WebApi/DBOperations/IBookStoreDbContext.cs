using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApi.Entities;



namespace WebApi.DBOperations;

public interface IBookStoreDbContext
{
    DbSet<Book> Books { get; set; }
    DbSet<Genre> Genres { get; set; }
    DbSet<Author> Authors { get; set; }
    public DbSet<User> Users { get; set; }

    int SaveChanges();
    EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;
    EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;


}

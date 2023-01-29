using Domain.Book;
using Domain.Account;
using Domain.Order;
using Microsoft.EntityFrameworkCore;

namespace BookOrder.Repositories
{
    public class BookOrderDbContext : DbContext
    {
        public BookOrderDbContext(DbContextOptions<BookOrderDbContext> options) : base(options)
        {}

        public DbSet<Author> Authors { get; set; }
        
        public DbSet<Publisher> Publishers { get; set; }
        
        public DbSet<Book> Books { get; set; }

        public DbSet<Account> Accounts { get; set; }
        
        public DbSet<Order> Orders { get; set; }
    }
}

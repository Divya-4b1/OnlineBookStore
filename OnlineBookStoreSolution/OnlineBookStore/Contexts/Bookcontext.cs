using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Models;

namespace OnlineBookStore.Contexts
{
    public class Bookcontext : DbContext
    {
        public Bookcontext(DbContextOptions options) : base(options)
        {

            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}

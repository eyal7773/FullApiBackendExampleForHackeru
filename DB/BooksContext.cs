using BooksApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.DB
{
    public class BooksContext : IdentityDbContext<ApplicationUser>
    {
        public BooksContext(DbContextOptions<BooksContext> options) : base(options)
        {
            
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseSqlServer("Server=.;Database=BookstoreAPI;Integrated Security=True");
        //    base.OnConfiguring(optionsBuilder);

        //}

        public DbSet<Book> Books { get; set; }

    }
}

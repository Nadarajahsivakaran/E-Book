using E_Book.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Book.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Book>().HasData(
               new Book
               {
                   Id = 1,
                   Title = "Harry Porter",
                   Author = "James Bond",
                   PublishedDate = DateTime.Now,
               },
               new Book
               {
                   Id = 2,
                   Title = "Makavaratham",
                   Author = "Ramesh Kumar",
                   PublishedDate = DateTime.Now,
               }
           );
            base.OnModelCreating(builder);
        }
    }
}

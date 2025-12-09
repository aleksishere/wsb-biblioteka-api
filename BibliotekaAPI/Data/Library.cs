using BibliotekaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotekaAPI.Data
{
    public class Library : DbContext
    {
        public Library(DbContextOptions<Library> options) : base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Copy> Copies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Autor -> Wiele Książek
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            // Książka -> Wiele Egzemplarzy
            modelBuilder.Entity<Books>()
                .HasMany(b => b.Copies)
                .WithOne(c => c.Book)
                .HasForeignKey(c => c.BookId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
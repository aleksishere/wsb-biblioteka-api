using BibliotekaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotekaAPI;

public class LibraryDb : DbContext
{
    public LibraryDb(DbContextOptions<LibraryDb> options) : base(options) {}
    public DbSet<Authors> Authors { get; set; } = default;
    public DbSet<Books> Books { get; set; } = default;
}
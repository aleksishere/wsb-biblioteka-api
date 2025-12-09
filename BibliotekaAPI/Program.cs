using BibliotekaAPI.DTOs;
using BibliotekaAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace BibliotekaAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<LibraryDb>(options => options.UseInMemoryDatabase("Library"));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        var app = builder.Build();
        
        // AUTORZY
        var authors = app.MapGroup("/authors");
        // GET /author - Pobieranie wszystkich autorów
        authors.MapGet("/", async (LibraryDb db) =>
            await db.Authors.Include(a => a.Books).Select(b => new ShowAuthor(b)).ToListAsync());
        
        // GET /author/{id} - Pobieranie autora
        authors.MapGet("/{id}", async (int id, LibraryDb db) =>
            await db.Authors.Include(c => c.Books).FirstOrDefaultAsync(a => a.Id == id)
                is Authors author
                ? Results.Ok(new ShowAuthor(author))
                : Results.NotFound());
        
        // GET /author/{id}/books - Pobieranie książek autora
        authors.MapGet("/{id}/books", async (int id, LibraryDb db) =>
        {
            var books = await db.Books
                .Include(b => b.Authors)
                .Where(b => b.AuthorId == id)
                .Select(b => new ShowBook(b))
                .ToListAsync();
            return Results.Ok(books);
        });
        
        // POST /author - Dodawanie autora
        authors.MapPost("/", async (CreateAuthor input, LibraryDb db) =>
        {
            var author = new Authors()
            {
                First_name = input.First_name,
                Last_name =  input.Last_name
            };
            db.Authors.Add(author);
            await db.SaveChangesAsync();
            return Results.Created($"/authors/{author.Id}", new ShowAuthor(author));
        });
        
        // PUT /author/{id} - Edytowanie autora
        authors.MapPut("/{id}", async (int id, CreateAuthor input, LibraryDb db) =>
        {
            var author = await db.Authors.FindAsync(id);
            if (author == null) return Results.NotFound();
            
            author.First_name = input.First_name;
            author.Last_name = input.Last_name;
            await db.SaveChangesAsync();
            return Results.NoContent();
        });
        
        // DELETE /author/{id} - Usuwanie autora
        authors.MapDelete("/{id}", async (int id, LibraryDb db) =>
        {
            if (await db.Authors.FindAsync(id) is Authors author)
            {
                db.Authors.Remove(author);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }

            return Results.NotFound();
        });
        
        // KSIĄZKI
        var books = app.MapGroup("/books");

        // GET /books - Pobieranie wszystkich ksiązek
        books.MapGet("/", async (LibraryDb db) =>
            await db.Books.Include(a => a.Authors).Select(b => new ShowBook(b)).ToListAsync());
        
        // GET /books/{id} - Pobieranie ksiązki
        books.MapGet("/{id}", async (int id, LibraryDb db) =>
            await db.Books.Include(c => c.Authors).FirstOrDefaultAsync(a => a.Id == id)
                is Books book
                ? Results.Ok(new ShowBook(book))
                : Results.NotFound());

        // POST /books - Dodawanie ksiązki
        books.MapPost("/", async (CreateBook input, LibraryDb db) =>
        {
            var authorExists = await db.Authors.AnyAsync(a => a.Id == input.AuthorId);
            if (!authorExists) return Results.BadRequest();
            
            var book = new Books()
            {
                Title = input.Title,
                Year =  input.Year
            };
            db.Books.Add(book);
            await db.SaveChangesAsync();
            var fullBook = await db.Books.Include(b => b.Authors).FirstAsync(b => b.Id == book.Id);
            return Results.Created($"/books/{book.Id}", new ShowBook(fullBook));
        });
        
        // PUT /books/{id} - Edytowanie ksiązki
        books.MapPut("/{id}", async (int id, CreateBook input, LibraryDb db) =>
        {
            var book = await db.Books.FindAsync(id);
            if (book == null) return Results.NotFound();
            
            book.Title = input.Title;
            book.Year = input.Year;
            book.AuthorId = input.AuthorId;
            await db.SaveChangesAsync();
            return Results.NoContent();
        });
        
        // DELETE /books/{id} - Usuwanie ksiązki
        books.MapDelete("/{id}", async (int id, LibraryDb db) =>
        {
            if (await db.Books.FindAsync(id) is Books book)
            {
                db.Books.Remove(book);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }

            return Results.NotFound();
        });
        
        app.Run();
    }
}
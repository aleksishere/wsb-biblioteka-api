using System.ComponentModel.DataAnnotations;
using BibliotekaAPI.Models;

namespace BibliotekaAPI.DTOs;

public class ShowBook
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [MaxLength(50)]
    public string Year { get; set; }
    [MaxLength(100)]
    public int AuthorId { get; set; }
    public virtual Authors? Authors { get; set; }
    
    public ShowBook() {}

    public ShowBook(Books books)
    {
        Title = books.Title;
        Year = books.Year;
        AuthorId = books.AuthorId;
        Authors = books.Authors;
    }
}
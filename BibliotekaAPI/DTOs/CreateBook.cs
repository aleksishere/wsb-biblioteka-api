using System.ComponentModel.DataAnnotations;

namespace BibliotekaAPI.DTOs;

public class CreateBook
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string Title { get; set; }
    public string Year { get; set; }
    [MaxLength(100)]
    public string Author { get; set; }
    public int AuthorId { get; set; }
}
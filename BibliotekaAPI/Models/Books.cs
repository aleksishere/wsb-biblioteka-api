using System.ComponentModel.DataAnnotations;

namespace BibliotekaAPI.Models;

public class Books
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
}
using System.ComponentModel.DataAnnotations;

namespace BibliotekaAPI.Models;

public class Authors
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string First_name { get; set; }
    [MaxLength(100)]
    public string Last_name { get; set; }

    public ICollection<Books> Books { get; set; } = new List<Books>();
}
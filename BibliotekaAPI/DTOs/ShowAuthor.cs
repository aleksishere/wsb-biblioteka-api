using System.ComponentModel.DataAnnotations;
using BibliotekaAPI.Models;

namespace BibliotekaAPI.DTOs;

public class ShowAuthor
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string First_name { get; set; }
    [MaxLength(100)]
    public string Last_name { get; set; }
    public ICollection<Books> Books { get; set; }
    
    public ShowAuthor() {}

    public ShowAuthor(Authors authors)
    {
        Id = authors.Id;
        First_name = authors.First_name;
        Last_name = authors.Last_name;
        Books = authors.Books;
    }
    
}
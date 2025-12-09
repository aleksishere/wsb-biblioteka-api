using System.ComponentModel.DataAnnotations;

namespace BibliotekaAPI.DTOs;

public class CreateAuthor
{
    [MaxLength(100)]
    public string First_name { get; set; }
    [MaxLength(100)]
    public string Last_name { get; set; }
}
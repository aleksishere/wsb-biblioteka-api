using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BibliotekaAPI.Models
{
    public class Books
    {
        public long Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; } = string.Empty;

        [Range(0, int.MaxValue)]
        public int Year { get; set; }
        
        public long AuthorId { get; set; }
        
        public Author? Author { get; set; }
        
        [JsonIgnore]
        public List<Copy> Copies { get; set; } = new();
    }
}
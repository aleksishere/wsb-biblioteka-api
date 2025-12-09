using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BibliotekaAPI.Models
{
    public class Author
    {
        public long Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false)]
        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = string.Empty;

        [JsonIgnore]
        public List<Books> Books { get; set; } = new();
    }
}
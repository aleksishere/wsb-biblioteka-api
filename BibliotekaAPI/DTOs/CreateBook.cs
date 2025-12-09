using System.ComponentModel.DataAnnotations;

namespace BibliotekaAPI.DTOs;

    public class CreateBook
    {
        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; } = string.Empty;

        [Range(0, int.MaxValue)]
        public int Year { get; set; }

        [Required]
        public long AuthorId { get; set; }
    }

    public class BookUpdateDto : CreateBook 
    {
        public long Id { get; set; }
    }

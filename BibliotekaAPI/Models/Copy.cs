using System.Text.Json.Serialization;

namespace BibliotekaAPI.Models
{
    public class Copy
    {
        public long Id { get; set; }
        public long BookId { get; set; }
        
        [JsonIgnore]
        public Books? Book { get; set; }
        
        public string InventoryNumber { get; set; } = Guid.NewGuid().ToString().Substring(0, 8);
    }
}
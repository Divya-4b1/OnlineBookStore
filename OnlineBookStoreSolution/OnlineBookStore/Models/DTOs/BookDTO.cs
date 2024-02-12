using System.ComponentModel.DataAnnotations;

namespace OnlineBookStore.Models.DTOs
{
    public class BookDTO
    {
        public int BookId { get; set; }
       
        public string Title { get; set; }
    
        public string Author { get; set; }
      
        public string Genere { get; set; }
        public DateTime PublishedDate { get; set; }

        public string? Username { get; set; }
    }
}

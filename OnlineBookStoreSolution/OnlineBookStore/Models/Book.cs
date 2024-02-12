using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineBookStore.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Genere {  get; set; }
        public DateTime PublishedDate { get; set;}

        public string Username { get; set; }
        [ForeignKey("Username")]
        public User? User { get; set; }

    }
}

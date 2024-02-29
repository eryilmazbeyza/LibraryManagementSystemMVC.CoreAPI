using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace coreWebApp.Models
{
    public class BookViewModel
    {
        [Key]
        public int BookId { get; set; }
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public int AuthorId { get; set; }
        public int PublisherId { get; set; }
        public int QuantityInStock { get; set; }
        public decimal Price { get; set; }
        public string? ISBN { get; set; }
        public string? Language { get; set; }
        public DateTime PublishedDate { get; set; }
        public string? BookImg { get; set; }

        // Navigation Properties
        [ForeignKey("AuthorId")]
        public AuthorViewModel? Author { get; set; }

        [ForeignKey("PublisherId")]
        public PublisherViewModel? Publisher { get; set; }
    }
}

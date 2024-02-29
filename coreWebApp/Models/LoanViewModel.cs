using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace coreWebApp.Models
{
    public class LoanViewModel
    {
        [Key]
        public int LoanId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int AdminId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }

        // Navigation Properties
        [ForeignKey("UserId")]
        public UserViewModel? User { get; set; }

        [ForeignKey("BookId")]
        public BookViewModel? Book { get; set; }

        [ForeignKey("AdminId")]
        public AdminViewModel? Admin { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coreModel
{
    public class Loan
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
        public User? User { get; set; }

        [ForeignKey("BookId")]
        public Book? Book { get; set; }

        [ForeignKey("AdminId")]
        public Admin? Admin { get; set; }
    }
}

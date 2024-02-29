using System.ComponentModel.DataAnnotations;

namespace coreWebApp.Models
{
    public class AdminViewModel
    {
        [Key]
        public int AdminId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}

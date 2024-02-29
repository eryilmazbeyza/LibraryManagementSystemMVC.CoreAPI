using System.ComponentModel.DataAnnotations;

namespace coreWebApp.Models
{
    public class UserViewModel
    {
        [Key]
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}

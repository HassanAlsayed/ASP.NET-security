using System.ComponentModel.DataAnnotations;

namespace WebApp.Entities
{
    public class RegisterModelView
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

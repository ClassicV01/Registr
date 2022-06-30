using System.ComponentModel.DataAnnotations;

namespace Registr.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter your Email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your password!")]
        [DataType(DataType.Password)]   
        public string Password { get; set; }
    }
}

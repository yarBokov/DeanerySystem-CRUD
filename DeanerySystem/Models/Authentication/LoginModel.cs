using System.ComponentModel.DataAnnotations;

namespace DeanerySystem.Models.Authentication
{
    public class LoginModel
    {
        [Required]
        public int? PersonId { get; set; }

        [Required, MinLength(8)]
        public string Password { get; set; }
    }
}

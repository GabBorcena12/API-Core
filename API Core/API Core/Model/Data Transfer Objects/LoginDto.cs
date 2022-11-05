using System.ComponentModel.DataAnnotations;

namespace API_Core.Model.Data_Transfer_Objects
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "Your Password is limited to {2} to {1} characters")]
        public string Password { get; set; }

    }
}

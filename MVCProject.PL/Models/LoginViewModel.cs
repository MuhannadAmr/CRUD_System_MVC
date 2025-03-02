using System.ComponentModel.DataAnnotations;

namespace MVCProject.PL.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RemeberMe { get; set; }
    }
}

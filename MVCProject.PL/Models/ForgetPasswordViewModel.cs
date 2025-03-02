using System.ComponentModel.DataAnnotations;

namespace MVCProject.PL.Models
{
	public class ForgetPasswordViewModel
	{
		[EmailAddress(ErrorMessage ="Invalid Email")]
		[Required(ErrorMessage ="Email Is Required")]
        public string Email { get; set; }
    }
}

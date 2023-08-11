using System.ComponentModel.DataAnnotations;

namespace KUSYS_Demo.Models
{
    public class UserForgotPasswordViewModel
    {
        [Display(Name = "e-Mail Address")]
        [Required(ErrorMessage = "Please Enter e-mail address")]
        public string Mail { get; set; }

    }
}
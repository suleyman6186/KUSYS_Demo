using System.ComponentModel.DataAnnotations;

namespace KUSYS_Demo.Models
{
    public class UserSignInViewModel
    {
        [Required(ErrorMessage ="Please Enter Username")]
        public string username { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        public string password { get; set; }
    }
}

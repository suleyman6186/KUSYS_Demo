using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KUSYS_Demo.Models
{
    public class UserSignUpViewModel
    {
        [Display(Name = "Student ID Number")]
        [Column(TypeName = "nvarchar(20)")]
        [Required(ErrorMessage = "Please Enter Student ID Number")]
        public string UserName { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage ="Please Enter First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please Enter Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Enter Birth Date")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }

        [Display(Name = "Password Again")]
        [Compare("Password", ErrorMessage = "Password not matching!")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "e-Mail Address")]
        [Required(ErrorMessage = "Please Enter e-mail address")]
        public string Mail { get; set; }

    }
}

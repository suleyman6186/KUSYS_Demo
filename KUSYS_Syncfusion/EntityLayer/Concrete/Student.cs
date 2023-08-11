using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Concrete
{
    public class Student : Entity
    {
        [Display(Name = "STUDENT ID")]
        [Column(TypeName = "nvarchar(20)")]
        public string StudentId { get; set; }


        [Display(Name = "FIRST NAME")]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }


        [Display(Name = "LAST NAME")]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }


        [Display(Name = "BIRTH DATE")]
        public DateTime BirthDate { get; set; }

        [NotMapped]
        public string Role { get; set; }
    }
}
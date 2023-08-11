using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Concrete
{
    public class Course : Entity
    {
        [Column(TypeName = "nvarchar(10)")]
        public string CourseId { get; set; }


        [Column(TypeName = "nvarchar(100)")]
        public string CourseName { get; set; }
    }
}
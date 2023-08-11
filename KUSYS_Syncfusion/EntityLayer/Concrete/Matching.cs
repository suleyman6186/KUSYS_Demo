using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Concrete
{
    public class Matching : Entity
    {
        public int StudentRecordID { get; set; }

        public int CourseRecordID { get; set; }

        public DateTime CreationDate { get; set; }

        [NotMapped]
        public string StudentDetail { get; set; }

        [NotMapped]
        public string CourseDetail { get; set; }

    }
}
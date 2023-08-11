using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength=1)]
        public string Name { get; set; }
        [Required]
        [StringLength(20, MinimumLength=1)]
        public string Surname { get; set; }
        [StringLength(20, MinimumLength=6)]
        public string Email { get; set; }
    }
}
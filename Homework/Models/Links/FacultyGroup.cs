using System.ComponentModel.DataAnnotations;

namespace Homework.Models.Links
{
    public class FacultyGroup
    {
        [Required]
        public int FacultyId { get; set; }
        [Key]
        [Required]
        public int GroupId { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace Homework.Models.Links
{
    public class GroupStudent
    {
        [Required]
        public int GroupId { get; set; }
        [Key]
        [Required]
        public int StudentId { get; set; }
    }
}
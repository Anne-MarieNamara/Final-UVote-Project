using System.ComponentModel.DataAnnotations;

namespace UVote.Models
{
    public class ContactModel
    {
        [Display(Name = "FullName")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Student ID")]
        [RegularExpression(@"^(\d{8})$")]
        [Required]
        public string StudentId { get; set; }

        [Required]
        public string Email { get; set; }

        [RegularExpression(@"^(\d{10})$")]
        [Required]
        public string PhoneNumber { get; set; }

        public string Comments { get; set; }
    }
}





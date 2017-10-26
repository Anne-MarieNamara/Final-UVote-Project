using System.ComponentModel.DataAnnotations;

namespace UVote.Models
{
    public class AdminModel
    {
        [Display(Name = "Employee ID")]
        [RegularExpression(@"^(\d{8})$")]
        [Required(ErrorMessage = "Invalid id")]
        public string EmployeeId { get; set; }

        [Required(ErrorMessage = "Name required")]
        public string Name { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password required")]
        [StringLength(10, ErrorMessage = "Minimum of 5 characters required", MinimumLength = 5)]
        public string Password { get; set; }
    }
}
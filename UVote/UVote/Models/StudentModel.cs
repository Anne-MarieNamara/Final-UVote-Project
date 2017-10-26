using System.ComponentModel.DataAnnotations;

namespace UVote.Models
{
    public class StudentModel
    {
        [Display(Name = "Student Number")]
        [RegularExpression(@"^(\d{8})$", ErrorMessage = "Invalid number")]
        [Required(ErrorMessage = "Number required")]
        public string StudentNumber { get; set; }

        [Display(Name = "First Name")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Invalid name")]
        [Required(ErrorMessage = "Name required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Invalid surname")]
        [Required(ErrorMessage = "Surname required")]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid phone number")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Invalid format")]
        [Required(ErrorMessage = "Number required")]
        public string Telephone { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password required")]
        [StringLength(10, ErrorMessage = "Invalid password length", MinimumLength = 5)]
        public string Password { get; set; }

        [Display(Name = "Address Line 1")]
        [Required(ErrorMessage = "Address line 1 required")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        [Required(ErrorMessage = "Address line 2 required")]
        public string AddressLine2 { get; set; }

        [Display(Name = "Address Line 3")]
        [Required(ErrorMessage = "Address line 3 required")]
        public string AddressLine3 { get; set; }

        [Display(Name = "Address Line 4")]
        [Required(ErrorMessage = "Address line 4 required")]
        public string AddressLine4 { get; set; }

        [Required]
        public string EmployeeId { get; set; }
    }
}